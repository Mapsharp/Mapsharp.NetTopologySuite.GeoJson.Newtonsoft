namespace Mapsharp.NetTopologySuite.GeoJson.Newtonsoft.Converters
{
    public interface ITypeConverter<in TFrom, out TTo>
    {
        TTo? Convert(TFrom? from);
    }
}
