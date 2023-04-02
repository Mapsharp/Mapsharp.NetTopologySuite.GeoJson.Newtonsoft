using Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters;
using NetTopologySuite.Geometries;


namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Tests;

public class PointConverterTests
{
    private readonly GeometryFactory _ntsGeometryFactory;
    private readonly PointConverter _converter;

    public PointConverterTests()
    {
        _ntsGeometryFactory = new GeometryFactory();
        _converter = new PointConverter(_ntsGeometryFactory);
    }


    [Fact]
    public void ReturnsNull_When_NTSPointNull()
    {
        var gjPoint = _converter.Convert((Point?)null);
        Assert.Null(gjPoint);
    }

    [Fact]
    public void ReturnsNull_When_GeoJsonPointNull()
    {
        var ntsPoint = _converter.Convert((Mapsharp.GeoJson.Core.Geometries.Point?)null);
        Assert.Null(ntsPoint);
    }

    [Theory]
    [InlineData(12.34, 89.3)]
    public void Returns2DPoint_When_2DNTSPoint(double x, double y)
    {
        var ntsPoint = _ntsGeometryFactory.CreatePoint(new Coordinate(x,y));
        var gjPoint = _converter.Convert(ntsPoint);

        Assert.NotNull(gjPoint);
        Assert.Equal(x, gjPoint.Coordinates.X);
        Assert.Equal(y, gjPoint.Coordinates.Y);

        Assert.Equal(2, gjPoint.GetCardinality());
    }
}