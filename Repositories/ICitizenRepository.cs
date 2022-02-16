using E_proc.Models;

namespace E_proc.Repositories
{
    public interface ICitizenRepository
    {
        Task<IEnumerable<Citizen>> ReadAsync();

        Task<Citizen> CreateAsync(Citizen citizen);

        Task<Citizen> ReadById(int id);

        Task<Citizen> UpdateAsync(int id, Citizen citizen);
        Task<int> Delete(int id);
    }
}
