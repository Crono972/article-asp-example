using Api.Repositories;

namespace Specs.Repositories;

public class JsonFileBoroughRepositoryTest
{
    [Test]
    public void Should_properly_load_the_13_borough_of_paris()
    {
        var repo = new JsonFileBoroughRepository();
        var borough = repo.Get(75013);
        Assert.NotNull(borough);
        Assert.That(borough.ArinseeCode, Is.EqualTo(75113));
        Assert.That(borough.BoroughNumber, Is.EqualTo(75013));
        Assert.That(borough.Townhall.Name, Is.EqualTo("Gobelins"));
    }

    [Test]
    public void Should_load_the_20_borough_of_paris()
    {
        var repo = new JsonFileBoroughRepository();
        var boroughs = repo.List();
        Assert.That(boroughs.Count, Is.EqualTo(20));
    }

}