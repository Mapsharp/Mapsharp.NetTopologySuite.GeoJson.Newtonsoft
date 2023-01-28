using NetTopologySuite.Geometries;


namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class MultiPointConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.MultiPoint, MultiPoint>,
                                       ITypeConverter<MultiPoint, Mapsharp.GeoJson.Core.Geometries.MultiPoint>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public MultiPointConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public MultiPoint? Convert(Mapsharp.GeoJson.Core.Geometries.MultiPoint? from)
        {
            if (from == null) return null;
            var coordinateSequence = CreateCoordinateSequence(from.Coordinates);
            return _ntsGeometryFactory.CreateMultiPoint(coordinateSequence);
        }

        public Mapsharp.GeoJson.Core.Geometries.MultiPoint? Convert(MultiPoint? from)
        {
            if (from == null) return null;
            var coordinates = from.Coordinates.Select(c => PositionConverter.ToPosition(c));
            return new Mapsharp.GeoJson.Core.Geometries.MultiPoint(coordinates);
        }

        private CoordinateSequence CreateCoordinateSequence(IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position> positions)
        {
            var coordinatesArray = positions.Select(p => PositionConverter.ToCoordinate(p)).ToArray();
            return _ntsGeometryFactory.CoordinateSequenceFactory.Create(coordinatesArray);
        }
    }
}
