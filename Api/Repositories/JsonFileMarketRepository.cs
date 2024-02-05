using Api.Model;
using Api.Model.GeoJson;
using Api.Repositories.Abstractions;
using Newtonsoft.Json.Linq;

namespace Api.Repositories;

public class JsonFileMarketRepository : IMarketRepository
{
    private readonly IList<Market> _inMemoryData = new List<Market>();
    private bool _isLoaded;
    private readonly object _locker = new();

    public JsonFileMarketRepository()
    {
        LoadData();
    }

    public Task<IList<Market>> GetFromAGivenBoroughAsync(int boroughNumber)
    {
        return Task.FromResult((IList<Market>)_inMemoryData.Where(m => m.BoroughLocation == boroughNumber).ToList());
    }

    public Task<IList<Market>> ListAsync()
    {
        return Task.FromResult((IList<Market>)_inMemoryData.OrderBy(m => m.BoroughLocation).ToList());
    }

    private void LoadData()
    {
        if (!_isLoaded)
        {
            lock (_locker)
            {
                if (!_isLoaded)
                {
                    var rawJson = EmbeddedFileRessourceHelper.GetFileContent("liste_des_marches_de_quartier_a_paris.json");
                    var tokens = JArray.Parse(rawJson);
                    var daysOfTheWeekds = new List<string>
                        { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" };
                    foreach (var token in tokens)
                    {
                        var market = new Market
                        {
                            BoroughLocation = token.SelectToken("fields.arrondissement")!.ToObject<int>(),
                            MarketAddress = token.SelectToken("fields.adresse_complete_poi_approchant")!.ToObject<string>(),
                            MarketCoordinates = token.SelectToken("geometry")!.ToObject<PointGeoJson>(),
                            MarketName = $"Marché {token.SelectToken("fields.marche")!.ToObject<string>()}",
                            MarketType = token.SelectToken("fields.type")!.ToObject<string>(),
                            MarketOccurences = new List<MarketPeriod>()
                        };

                        foreach (var day in daysOfTheWeekds)
                        {
                            var openHours = token.SelectToken($"fields.{day}");
                            if (openHours != null)
                            {

                                market.MarketOccurences.Add(new MarketPeriod
                                {
                                    Day = day,
                                    OpenHours = openHours.ToObject<string>()
                                });
                            }
                        }
                        _inMemoryData.Add(market);
                    }

                    _isLoaded = true;
                }
            }
        }
    }
}