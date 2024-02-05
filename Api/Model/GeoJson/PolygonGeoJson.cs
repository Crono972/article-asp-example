namespace Api.Model.GeoJson;

public class PolygonGeoJson : GeoJsonBaseObject
{
    public IList<IList<IList<decimal>>> Coordinates { get; set; }
}