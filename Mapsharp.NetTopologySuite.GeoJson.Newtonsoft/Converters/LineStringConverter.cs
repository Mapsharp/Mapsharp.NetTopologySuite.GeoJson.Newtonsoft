using NetTopologySuite.Geometries;


namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class LineStringConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.LineString, LineString>,
                                       ITypeConverter<LineString, Mapsharp.GeoJson.Core.Geometries.LineString>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public LineStringConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public LineString? Convert(Mapsharp.GeoJson.Core.Geometries.LineString? from)
        {
            if (from == null) return null;
            var coordinateSequence = CreateCoordinateSequence(from.Coordinates);
            return _ntsGeometryFactory.CreateLineString(coordinateSequence);
        }

        public Mapsharp.GeoJson.Core.Geometries.LineString? Convert(LineString? from)
        {
            if (from == null) return null;
            var coordinates = from.Coordinates.Select(c => PositionConverter.ToPosition(c));
            return new Mapsharp.GeoJson.Core.Geometries.LineString(coordinates);
        }

        private CoordinateSequence CreateCoordinateSequence(IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position> positions)
        {
            var coordinatesArray = positions.Select(p => PositionConverter.ToCoordinate(p)).ToArray();
            return _ntsGeometryFactory.CoordinateSequenceFactory.Create(coordinatesArray);
        }
    }
}
