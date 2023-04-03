using Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.ComponentModel;

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
            settings.Converters.Add(CreatePolygonJsonConverter(factory));
            settings.Converters.Add(CreateMultiPolygonJsonConverter(factory));
            settings.Converters.Add(CreateGeometryCollectionJsonConverter(factory));

            return settings;
        }

        private static TypeMappingJsonConverter<TType, TJsonType> CreateJsonConverter<TType, TJsonType, TConverterType>(TConverterType converter)
            where TConverterType : ITypeConverter<TType, TJsonType>, ITypeConverter<TJsonType, TType>
        {
            return new TypeMappingJsonConverter<TType, TJsonType>(converter, converter);
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

        private static TypeMappingJsonConverter<Polygon, Mapsharp.GeoJson.Core.Geometries.Polygon> CreatePolygonJsonConverter(GeometryFactory factory)
        {
            var polygonConverter = new PolygonConverter(factory);
            return new TypeMappingJsonConverter<Polygon, Mapsharp.GeoJson.Core.Geometries.Polygon>(polygonConverter, polygonConverter);
        }

        private static TypeMappingJsonConverter<MultiPolygon, Mapsharp.GeoJson.Core.Geometries.MultiPolygon> CreateMultiPolygonJsonConverter(GeometryFactory factory)
        {
            var multiPolygonConverter = new MultiPolygonConverter(factory);
            return new TypeMappingJsonConverter<MultiPolygon, Mapsharp.GeoJson.Core.Geometries.MultiPolygon>(multiPolygonConverter, multiPolygonConverter);
        }

        private static TypeMappingJsonConverter<GeometryCollection, Mapsharp.GeoJson.Core.Geometries.GeometryCollection> CreateGeometryCollectionJsonConverter(GeometryFactory factory)
        {
            var geometryCollectionConverter = new GeometryCollectionConverter(factory);
            return new TypeMappingJsonConverter<GeometryCollection, Mapsharp.GeoJson.Core.Geometries.GeometryCollection>(geometryCollectionConverter, geometryCollectionConverter);
        }
    }
}
