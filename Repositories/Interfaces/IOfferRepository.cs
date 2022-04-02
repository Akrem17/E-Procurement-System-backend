using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> ReadAsync(int skip, int take);

        Task<Offer> CreateAsync(Offer offer);

        Task<Offer> ReadById(int id);

        Task<Offer> UpdateAsync(int id, Offer offer);

        Task<int> Delete(int id);
        public int CountData();
        Task<List<Offer>> FindBy(string? supplierId, string? supplierEmail);


    }
}
