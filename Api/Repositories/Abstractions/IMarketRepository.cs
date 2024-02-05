using Api.Model;

namespace Api.Repositories.Abstractions;

public interface IMarketRepository
{
    public Task<IList<Market>> GetFromAGivenBoroughAsync(int boroughNumber);
    public Task<IList<Market>> ListAsync();
}