using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface IRepresentativeRepository
    {


        Task<IEnumerable<Representative>> ReadAsync();

        Task<Representative> CreateAsync(Representative representative);

        Task<Representative> ReadById(int id);

        Task<Representative> UpdateAsync(int id, Representative representative);
        Task<List<Representative>> FindBy(string? socialSecurityNumber);

        Task<int> Delete(int id);

    }
}
