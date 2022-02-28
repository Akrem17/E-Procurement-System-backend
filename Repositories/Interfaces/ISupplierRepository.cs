using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> ReadAsync();

        Task<Supplier> CreateAsync(Supplier supplier);

        Task<Supplier> ReadById(int id);

        Task<Supplier> UpdateAsync(int id, Supplier supplier);
        Task<List<Supplier>> FindBy(string? email, bool? confirmed, string? phone);

        Task<int> Delete(int id);
    }
}
