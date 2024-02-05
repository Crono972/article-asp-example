using Api.Model;

namespace Api.Repositories.Abstractions;

public interface IBoroughRepository
{
    public Task<Borough> GetAsync(int boroughNumber);
    public Task<IList<Borough>> ListAsync();
}