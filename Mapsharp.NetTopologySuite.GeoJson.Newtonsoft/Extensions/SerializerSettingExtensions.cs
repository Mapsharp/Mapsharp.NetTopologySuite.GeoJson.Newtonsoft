using Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Extensions
{
    public static class SerializerSettingExtensions
    {
        public static JsonSerializerSettings AddNetTopologySuiteConverters(this JsonSerializerSettings settings, GeometryFactory factory)
        {
            settings.Converters.Add(CreatePointConverter(factory));

            return settings;
        }

        private static TypeMappingJsonConverter<Point, Mapsharp.GeoJson.Core.Geometries.Point> CreatePointConverter(GeometryFactory factory)
        {
            var pointConverter = new PointConverter(factory);
            return new TypeMappingJsonConverter<Point, Mapsharp.GeoJson.Core.Geometries.Point>(pointConverter, pointConverter);
        }
    }
}
