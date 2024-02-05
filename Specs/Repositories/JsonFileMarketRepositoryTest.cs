using Api.Repositories;

namespace Specs.Repositories;

public class JsonFileMarketRepositoryTest
{
    [Test]
    public void Should_properly_load_markets()
    {
        var repo = new JsonFileMarketRepository();
        var markets = repo.List();
        Assert.That(markets.Count, Is.EqualTo(94));
    }

    [Test]
    public void Should_properly_return_market_of_enfants_rouge()
    {
        var repo = new JsonFileMarketRepository();
        var markets = repo.GetFromAGivenBorough(75003);
        Assert.That(markets.Count, Is.EqualTo(1));
        var market = markets.Single(m => m.MarketName == "Marché Enfants rouges");
        Assert.NotNull(market);
        Assert.That(market.MarketAddress, Is.EqualTo("33, Rue de Bretagne 75003 Paris, France"));
    }
}