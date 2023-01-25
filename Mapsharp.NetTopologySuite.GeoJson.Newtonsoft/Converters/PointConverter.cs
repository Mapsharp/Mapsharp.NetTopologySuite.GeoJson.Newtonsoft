using NetTopologySuite.Geometries;


namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class PointConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.Point, Point>,
                                  ITypeConverter<Point, Mapsharp.GeoJson.Core.Geometries.Point>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public PointConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public Point? Convert(Mapsharp.GeoJson.Core.Geometries.Point? from)
        {
            if (from == null) return null;
            var coordinate = PositionConverter.ToCoordinate(from.Coordinates);
            return _ntsGeometryFactory.CreatePoint(coordinate);
        }

        public Mapsharp.GeoJson.Core.Geometries.Point? Convert(Point? from)
        {
            if (from == null) return null;
            var position = PositionConverter.ToPosition(from.Coordinate);
            return new Mapsharp.GeoJson.Core.Geometries.Point(position);
        }
    }
}
