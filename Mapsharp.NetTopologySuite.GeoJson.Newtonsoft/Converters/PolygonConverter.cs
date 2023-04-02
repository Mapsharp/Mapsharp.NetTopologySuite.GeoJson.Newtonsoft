using NetTopologySuite.Geometries;
using NetTopologySuite.GeometriesGraph;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class PolygonConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.Polygon, Polygon>,
                                    ITypeConverter<Polygon, Mapsharp.GeoJson.Core.Geometries.Polygon>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public PolygonConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public Polygon? Convert(Mapsharp.GeoJson.Core.Geometries.Polygon? from)
        {
            if (from == null) return null;
            var shell = CreateLinearRing(from.Coordinates.First());
            var holes = from.Coordinates.Skip(1).Select(cs => CreateLinearRing(cs)).ToArray(); 
            return _ntsGeometryFactory.CreatePolygon(shell, holes);
        }

        public Mapsharp.GeoJson.Core.Geometries.Polygon? Convert(Polygon? from)
        {
            if (from == null) return null;
            var shell = from.Shell.Coordinates.Select(c => PositionConverter.ToPosition(c));
            var holes = from.Holes.Select(h => h.Coordinates.Select(c => PositionConverter.ToPosition(c)));            
            return new Mapsharp.GeoJson.Core.Geometries.Polygon(holes.Prepend(shell));
        }

        private LinearRing CreateLinearRing(IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position> positions)
        {
            var coordinatesArray = positions.Select(p => PositionConverter.ToCoordinate(p)).ToArray();
            return _ntsGeometryFactory.CreateLinearRing(coordinatesArray);
        }
    }
}
