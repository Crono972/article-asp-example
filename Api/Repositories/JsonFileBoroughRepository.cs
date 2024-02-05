using Api.Model;
using Api.Model.GeoJson;
using Api.Repositories.Abstractions;
using Newtonsoft.Json.Linq;

namespace Api.Repositories;

public class JsonFileBoroughRepository : IBoroughRepository
{
    private readonly IList<Borough> _inMemoryData = new List<Borough>();
    private bool _isLoaded;
    private readonly object _locker = new();

    public JsonFileBoroughRepository()
    {
        LoadData();
    }

    public Task<Borough> GetAsync(int boroughNumber)
    {
        return Task.FromResult(_inMemoryData.FirstOrDefault(b => b.BoroughNumber == boroughNumber));
    }

    public Task<IList<Borough>> ListAsync()
    {
        return Task.FromResult((IList<Borough>)_inMemoryData.OrderBy(b => b.BoroughNumber).ToList());
    }

    private void LoadData()
    {
        if (!_isLoaded)
        {
            lock (_locker)
            {
                if (!_isLoaded)
                {
                    var rawJson = EmbeddedFileRessourceHelper.GetFileContent("arrondissements.json");
                    var tokens = JArray.Parse(rawJson);
                    foreach (var token in tokens)
                    {
                        var borough = new Borough
                        {
                            ArinseeCode = token.SelectToken("fields.c_arinsee")!.ToObject<int>(),
                            BoroughNumber = token.SelectToken("fields.c_ar")!.ToObject<int>() + 75_000,
                            Coordinates = token.SelectToken("fields.geom")!.ToObject<PolygonGeoJson>(),
                            Name = token.SelectToken("fields.l_ar")!.ToObject<string>(),
                            PerimeterSize = token.SelectToken("fields.perimetre")!.ToObject<decimal>(),
                            Townhall = new TownHall
                            {
                                Name = token.SelectToken("fields.l_aroff")!.ToObject<string>(),
                                Coordinates = token.SelectToken("geometry")!.ToObject<PointGeoJson>()
                            }
                        };

                        _inMemoryData.Add(borough);
                    }

                    _isLoaded = true;
                }
            }
        }
    }
}