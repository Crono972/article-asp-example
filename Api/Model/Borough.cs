using Api.Model.GeoJson;

namespace Api.Model;

public class Borough
{
    public string Name { get; set; }
    public int BoroughNumber { get; set; }
    public int ArinseeCode { get; set; }
    public decimal PerimeterSize { get; set; }
    public TownHall Townhall { get; set; }
    public PolygonGeoJson Coordinates { get; set; }
}