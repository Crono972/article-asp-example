using Api.Model.GeoJson;

namespace Api.Model;

public class Market
{
    public string MarketName { get; set; }
    public int BoroughLocation { get; set; }
    public string MarketAddress { get; set; }
    public PointGeoJson MarketCoordinates { get; set; }
    public IList<MarketPeriod> MarketOccurences { get; set; }
    public string MarketType { get; set; }
}