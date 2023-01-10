using Newtonsoft.Json;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class TypeMappingJsonConverter<TType, TJsonType> : JsonConverter<TType>
    {
        private readonly ITypeConverter<TType, TJsonType> _toJsonTypeConverter;
        private readonly ITypeConverter<TJsonType, TType> _fromJsonTypeConverter;

        public TypeMappingJsonConverter(ITypeConverter<TType, TJsonType> toJsonTypeConverter,
                                        ITypeConverter<TJsonType, TType> fromJsonTypeConverter)
        {
            _toJsonTypeConverter = toJsonTypeConverter;
            _fromJsonTypeConverter = fromJsonTypeConverter;
        }

        public override TType? ReadJson(JsonReader reader, Type objectType, TType? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            TJsonType? jsonType = serializer.Deserialize<TJsonType>(reader);
            return _fromJsonTypeConverter.Convert(jsonType);
        }

        public override void WriteJson(JsonWriter writer, TType? value, JsonSerializer serializer)
        {
            TJsonType? jsonType = _toJsonTypeConverter.Convert(value);
            serializer.Serialize(writer, jsonType);
        }
    }

}
