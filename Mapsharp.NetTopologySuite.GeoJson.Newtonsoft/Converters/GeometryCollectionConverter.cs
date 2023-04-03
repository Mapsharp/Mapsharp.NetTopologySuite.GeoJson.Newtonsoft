using NetTopologySuite.Geometries;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    internal class GeometryCollectionConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.GeometryCollection, GeometryCollection>,
                                                 ITypeConverter<GeometryCollection, Mapsharp.GeoJson.Core.Geometries.GeometryCollection>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        private readonly PointConverter _pointConverter;
        private readonly MultiPointConverter _multiPointConverter;
        private readonly LineStringConverter _lineStringConverter;
        private readonly MultiLineStringConverter _multiLineStringConverter;
        private readonly PolygonConverter _polygonConverter;
        private readonly MultiPolygonConverter _multiPolygonConverter;

        public GeometryCollectionConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
            _pointConverter = new(_ntsGeometryFactory);
            _multiPointConverter = new(_ntsGeometryFactory);
            _lineStringConverter = new(_ntsGeometryFactory);
            _multiLineStringConverter = new(_ntsGeometryFactory);
            _polygonConverter = new(_ntsGeometryFactory);
            _multiPolygonConverter = new(_ntsGeometryFactory);
        }

        public GeometryCollection? Convert(Mapsharp.GeoJson.Core.Geometries.GeometryCollection? from)
        {
            if (from == null) return null;
            var geometries = from.Geometries.Select(g => ConvertGeometry(g)).ToArray();
            return _ntsGeometryFactory.CreateGeometryCollection(geometries);
        }

        public Mapsharp.GeoJson.Core.Geometries.GeometryCollection? Convert(GeometryCollection? from)
        {
            if (from == null) return null;
            var geometries = from.Geometries.Select(g => ConvertGeometry(g)).ToArray();
            return new Mapsharp.GeoJson.Core.Geometries.GeometryCollection(geometries);
        }

        private Geometry ConvertGeometry(Mapsharp.GeoJson.Core.Geometries.GeometryBase g)
        {
            switch(g)
            {
                case Mapsharp.GeoJson.Core.Geometries.Point point:
                    return _pointConverter.Convert(point)!;
                case Mapsharp.GeoJson.Core.Geometries.MultiPoint multiPoint:
                    return _multiPointConverter.Convert(multiPoint)!;
                case Mapsharp.GeoJson.Core.Geometries.LineString lineString:
                    return _lineStringConverter.Convert(lineString)!;
                case Mapsharp.GeoJson.Core.Geometries.MultiLineString multiLineString:
                    return _multiLineStringConverter.Convert(multiLineString)!;
                case Mapsharp.GeoJson.Core.Geometries.Polygon polygon:
                    return _polygonConverter.Convert(polygon)!;
                case Mapsharp.GeoJson.Core.Geometries.MultiPolygon multiPolygon:
                    return _multiPolygonConverter.Convert(multiPolygon)!;
                case Mapsharp.GeoJson.Core.Geometries.GeometryCollection geometryCollection:
                    return Convert(geometryCollection)!;
            }

            throw new NotImplementedException();
        }

        private Mapsharp.GeoJson.Core.Geometries.GeometryBase ConvertGeometry(Geometry g)
        {
            switch(g)
            {
                case Point point:
                    return _pointConverter.Convert(point)!;
                case MultiPoint multiPoint:
                    return _multiPointConverter.Convert(multiPoint)!;
                case LineString lineString:
                    return _lineStringConverter.Convert(lineString)!;
                case MultiLineString multiLineString:
                    return _multiLineStringConverter.Convert(multiLineString)!;
                case Polygon polygon:
                    return _polygonConverter.Convert(polygon)!;
                case MultiPolygon multiPolygon:
                    return _multiPolygonConverter.Convert(multiPolygon)!;
                case GeometryCollection geometryCollection:
                    return Convert(geometryCollection)!;
            }

            throw new NotImplementedException();
        }
    }
}
