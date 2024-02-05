namespace Api.Model.GeoJson;

public class PointGeoJson : GeoJsonBaseObject
{
    public IList<decimal> Coordinates { get; set; }
}