using NetTopologySuite.Geometries;


namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public class MultiLineStringConverter : ITypeConverter<Mapsharp.GeoJson.Core.Geometries.MultiLineString, MultiLineString>,
                                            ITypeConverter<MultiLineString, Mapsharp.GeoJson.Core.Geometries.MultiLineString>
    {
        private readonly GeometryFactory _ntsGeometryFactory;
        public MultiLineStringConverter(GeometryFactory geometryFactory)
        {
            _ntsGeometryFactory = geometryFactory;
        }

        public MultiLineString? Convert(Mapsharp.GeoJson.Core.Geometries.MultiLineString? from)
        {
            if (from == null) return null;
            var lineStrings = from.Coordinates.Select(c => CreateLineString(c)).ToArray();
            return _ntsGeometryFactory.CreateMultiLineString(lineStrings);
        }

        public Mapsharp.GeoJson.Core.Geometries.MultiLineString? Convert(MultiLineString? from)
        {
            if (from == null) return null;
            var coordinates = from.Select(ls => ls.Coordinates.Select(c => PositionConverter.ToPosition(c))).ToArray();       
            return new Mapsharp.GeoJson.Core.Geometries.MultiLineString(coordinates);
        }

        private LineString CreateLineString(IEnumerable<Mapsharp.GeoJson.Core.Geometries.Position> positions)
        {
            var coordinatesArray = positions.Select(p => PositionConverter.ToCoordinate(p)).ToArray();
            var coordinateSequence = _ntsGeometryFactory.CoordinateSequenceFactory.Create(coordinatesArray);
            return _ntsGeometryFactory.CreateLineString(coordinateSequence);
        }
    }
}
