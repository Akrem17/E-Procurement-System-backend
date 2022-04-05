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
        int CountDataWithFilters(int skip, int take, string? supplierId, string? supplierEmail, string? offerNumber = null, string? tenderName = null, string? city = null, string? postDate = null);

        Task<List<Offer>> FindBy(int skip, int take ,string? supplierId, string? supplierEmail,  string? offerNumber = null, string? tenderName = null, string? city = null, string? postDate = null);


    }
}
