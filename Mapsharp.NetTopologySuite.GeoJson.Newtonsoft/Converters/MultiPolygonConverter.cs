using NetTopologySuite.Geometries;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class MultiPolygonConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.MultiPolygon, MultiPolygon>,
                                         ITypeConverter<MultiPolygon, Mapsharp.GeoJson.Core.Geometries.MultiPolygon>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public MultiPolygonConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public MultiPolygon? Convert(Mapsharp.GeoJson.Core.Geometries.MultiPolygon? from)
        {
            if (from == null) return null;
            var polygons = from.Coordinates.Select(cs => CreatePolygon(cs)).ToArray();
            return _ntsGeometryFactory.CreateMultiPolygon(polygons);
        }

        public Mapsharp.GeoJson.Core.Geometries.MultiPolygon? Convert(MultiPolygon? from)
        {
            if (from == null) return null;
            var coordinates = from.Geometries.Cast<Polygon>().Select(p => CreateCoordinates(p)).ToArray();     
            return new Mapsharp.GeoJson.Core.Geometries.MultiPolygon(coordinates);
        }

        private IEnumerable<IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position>> CreateCoordinates(Polygon p)
        {
            var shell = p.Shell.Coordinates.Select(c => PositionConverter.ToPosition(c));
            var holes = p.Holes.Select(h => h.Coordinates.Select(c => PositionConverter.ToPosition(c)));
            return holes.Prepend(shell);
        }

        private Polygon CreatePolygon(IEnumerable<IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position>> positions)
        {
            var shell = CreateLinearRing(positions.First());
            var holes = positions.Skip(1).Select(cs => CreateLinearRing(cs)).ToArray();
            return _ntsGeometryFactory.CreatePolygon(shell, holes);
        }
        private LinearRing CreateLinearRing(IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position> positions)
        {
            var coordinatesArray = positions.Select(p => PositionConverter.ToCoordinate(p)).ToArray();
            return _ntsGeometryFactory.CreateLinearRing(coordinatesArray);
        }

    }
}
