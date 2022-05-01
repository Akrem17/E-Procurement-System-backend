using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface ITenderClassificationRepository
    {
        Task<TenderClassification> UpdateAsync(int id, TenderClassification address);
        Task<TenderClassification> ReadById(int id);

    }
}
