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

        public Task<List<Offer>> FindBy(string? bidNumber, string? bidName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Offer>> ReadAsync(int skip, int take)
        {
            var tender = await _dbContext.Offer.Include(o => o.Supplier).Include(o => o.Tender).Include(o => o.Files).Skip(skip).Take(take).ToArrayAsync();



            return tender;
        }

        public async Task<Offer> ReadById(int id)
        {
            return await _dbContext.Offer.Include(o=>o.Files) .Include(o=>o.Supplier).ThenInclude(s=>s.address). FirstOrDefaultAsync(ad => ad.Id == id);

        }

        public Task<Offer> UpdateAsync(int id, Offer offer)
        {
            throw new NotImplementedException();
        }
    }
}
