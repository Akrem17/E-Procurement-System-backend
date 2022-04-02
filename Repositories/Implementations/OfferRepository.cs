using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AuthContext _dbContext;
        public OfferRepository(AuthContext dbContext)
        {

            _dbContext = dbContext;
        }

        public int CountData()
        {
            return _dbContext.Offer.Count();
        }

        public Task<Offer> CreateAsync(Offer offer)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Offer>> FindBy(string? supplierId, string? supplierEmail)
        {
            var offers = await _dbContext.Offer
                                .Where(s => !string.IsNullOrEmpty(supplierId) ? s.SupplierId.ToString() == supplierId : true)
                                .Where(s => !string.IsNullOrEmpty(supplierEmail) ? s.Supplier.Email == supplierEmail : true)
                                 .Include(t => t.Supplier)
                                .ToListAsync();
            Tender tender;
            for (var i = 0; i < offers.Count; i++)
            {
                tender = await _dbContext.Tender.Where(t => t.Id == offers[i].TenderId).FirstOrDefaultAsync();
                tender.Offers = null;
                offers[i].TenderInfo = tender;
            }


            return offers;
        }

        public async Task<IEnumerable<Offer>> ReadAsync(int skip, int take)
        {
            var offers = await _dbContext.Offer.Include(o => o.Supplier).Include(o => o.Tender).Include(o => o.Files).Skip(skip).Take(take).ToArrayAsync();



            return offers;
        }

        public async Task<Offer> ReadById(int id)
        {
            return await _dbContext.Offer.Include(o => o.Files).Include(o => o.Supplier).ThenInclude(s => s.address).FirstOrDefaultAsync(ad => ad.Id == id);

        }

        public async Task<Offer> UpdateAsync(int id, Offer offer)
        {

            var oldOffer = await ReadById(id);

            if (oldOffer != null)
            {
                oldOffer.Name = offer.Name;oldOffer.TotalMontant = offer.TotalMontant;offer.FinalTotalMontant = offer.FinalTotalMontant;offer.isAccepted = offer.isAccepted; offer.SupplierId = offer.SupplierId;
                
                await _dbContext.SaveChangesAsync();
                return oldOffer;
            }

            return null;


        }
    }
    }

