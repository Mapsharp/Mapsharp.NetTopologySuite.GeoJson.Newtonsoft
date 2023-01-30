using Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Extensions
{
    public static class SerializerSettingExtensions
    {
        public static JsonSerializerSettings AddNetTopologySuiteConverters(this JsonSerializerSettings settings, GeometryFactory factory)
        {
            settings.Converters.Add(CreatePointJsonConverter(factory));
            settings.Converters.Add(CreateMultiPointJsonConverter(factory));
            settings.Converters.Add(CreateLineStringJsonConverter(factory));
            settings.Converters.Add(CreateMultiLineStringJsonConverter(factory));

            return settings;
        }

        private static TypeMappingJsonConverter<Point, Mapsharp.GeoJson.Core.Geometries.Point> CreatePointJsonConverter(GeometryFactory factory)
        {
            var pointConverter = new PointConverter(factory);
            return new TypeMappingJsonConverter<Point, Mapsharp.GeoJson.Core.Geometries.Point>(pointConverter, pointConverter);
        }

        private static TypeMappingJsonConverter<MultiPoint, Mapsharp.GeoJson.Core.Geometries.MultiPoint> CreateMultiPointJsonConverter(GeometryFactory factory)
        {
            var multiPointConverter = new MultiPointConverter(factory);
            return new TypeMappingJsonConverter<MultiPoint, Mapsharp.GeoJson.Core.Geometries.MultiPoint>(multiPointConverter, multiPointConverter);
        }

        private static TypeMappingJsonConverter<LineString, Mapsharp.GeoJson.Core.Geometries.LineString> CreateLineStringJsonConverter(GeometryFactory factory)
        {
            var lineStringConverter = new LineStringConverter(factory);
            return new TypeMappingJsonConverter<LineString, Mapsharp.GeoJson.Core.Geometries.LineString>(lineStringConverter, lineStringConverter);
        }

        private static TypeMappingJsonConverter<MultiLineString, Mapsharp.GeoJson.Core.Geometries.MultiLineString> CreateMultiLineStringJsonConverter(GeometryFactory factory)
        {
            var multiLineStringConverter = new MultiLineStringConverter(factory);
            return new TypeMappingJsonConverter<MultiLineString, Mapsharp.GeoJson.Core.Geometries.MultiLineString>(multiLineStringConverter, multiLineStringConverter);
        }
    }
}
