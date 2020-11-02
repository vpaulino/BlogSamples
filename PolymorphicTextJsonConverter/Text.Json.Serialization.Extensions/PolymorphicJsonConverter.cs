using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Text.Json.Serialization.Extensions
{

    public class PolymorphicJsonConverter<T> : JsonConverter<T> where T : class
    {
        protected string TypeDescriminator { get; }
        
        private Dictionary<string, Func<string, Type>> typeResolvers;

        public PolymorphicJsonConverter(Dictionary<string, Func<string, Type>> typeResolver, string typeDescriminator)
        {
            this.TypeDescriminator = typeDescriminator;
            this.typeResolvers = typeResolver ?? new Dictionary<string, Func<string, Type>>();
        }

        public PolymorphicJsonConverter(string typeDescriminator)
        {
            this.TypeDescriminator = typeDescriminator;
            this.typeResolvers = new Dictionary<string, Func<string, Type>>();
            SetAllKnownTypes();
        }

        protected virtual void SetAllKnownTypes()
        {
            var allTypes = FindDerivedTypes(typeof(T).Assembly, typeof(T));

            foreach (var type in allTypes)
            {
                this.typeResolvers.Add(type.FullName, (descriminator) => type);
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }



        protected virtual void AddTypes(string fullName, Func<string, Type> resolverHandler) 
        {
            this.typeResolvers.Add(fullName, resolverHandler);
        }

        protected IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => t != baseType &&
                                                  baseType.IsAssignableFrom(t));
        }


        protected virtual  T Deserialize(ref Utf8JsonReader reader, string typeDiscriminatorValue)
        {
            if (!typeResolvers.TryGetValue(typeDiscriminatorValue, out var typeResolver))
                throw new ArgumentOutOfRangeException();
            T result = JsonSerializer.Deserialize(ref reader, typeResolver(typeDiscriminatorValue)) as T;
            return result;
        }



        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
          

            var beginnerReader = reader;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || reader.GetString() != TypeDescriminator)
            {
                throw new JsonException();
            }

            T baseClass;
            if( !((string)reader.GetString()).Equals(this.TypeDescriminator) || !reader.Read())
                throw new JsonException();
            
            var typeDescriminatorValue = reader.GetString();
            baseClass = Deserialize(ref beginnerReader, typeDescriminatorValue);

            if (beginnerReader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            reader = beginnerReader;

            return baseClass;
        }

        
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {

            var bytes = JsonSerializer.SerializeToUtf8Bytes(value, value.GetType());
            var spanOfValue = bytes.AsSpan();

            ReadOnlySpan<byte> descriptor = UTF8Encoding.UTF8.GetBytes($"{{ \"{this.TypeDescriminator}\" : \"{value.GetType().FullName}\", ").AsSpan();
            var length = descriptor.Length + (spanOfValue.Length);
            Span<byte> totalObjectJson = stackalloc byte[length];

            int currentIndex = 0;
            descriptor.CopyTo(totalObjectJson.Slice(currentIndex, descriptor.Length));
            currentIndex += descriptor.Length;

            var objectToCopy = spanOfValue.Slice(1, spanOfValue.Length - 1);
            objectToCopy.CopyTo(totalObjectJson.Slice(currentIndex, spanOfValue.Length));
            var parsedBytes = JsonDocument.Parse(totalObjectJson.Slice(0, totalObjectJson.Length - 1).ToArray());

            parsedBytes.RootElement.WriteTo(writer);


        }
    }
}
