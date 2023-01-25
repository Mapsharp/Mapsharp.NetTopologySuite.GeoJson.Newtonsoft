using NetTopologySuite.Geometries;

namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public static class PositionConverter
    {
        public static Coordinate ToCoordinate(Mapsharp.GeoJson.Core.Geometries.Position position)
        {
            if(position.Cardinality == 3)
            {
                return new CoordinateZ(position.X, position.Y, position.Z);
            }

            return new Coordinate(position.X, position.Y);
        }

        public static Mapsharp.GeoJson.Core.Geometries.Position ToPosition(Coordinate coordinate)
        {
            if (coordinate == null) throw new ArgumentNullException(nameof(coordinate));
            
            double z = coordinate.Z;

            if(double.IsNaN(z))
            {
                return new Mapsharp.GeoJson.Core.Geometries.Position(coordinate.X, coordinate.Y);
            }

            return new Mapsharp.GeoJson.Core.Geometries.Position(coordinate.X, coordinate.Y, z);
        }
    }
}
