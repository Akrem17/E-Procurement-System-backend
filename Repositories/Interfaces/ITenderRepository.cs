using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface ITenderRepository
    {
        Task<IEnumerable<Tender>> ReadAsync(int skip,int take);

        Task<Tender> CreateAsync(Tender tender);

        Task<Tender> ReadById(int id);

        Task<Tender> UpdateAsync(int id, Tender tender);
        Task<List<Tender>> FindBy(string? email, bool? confirmed, string? phone, DateTime? date);

        Task<int> Delete(int id);
        public int CountData();
    }
}
