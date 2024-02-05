using Api.Model.GeoJson;

namespace Api.Model;

public class TownHall
{
    public string Name { get; set; }
    public PointGeoJson Coordinates { get; set; }

}