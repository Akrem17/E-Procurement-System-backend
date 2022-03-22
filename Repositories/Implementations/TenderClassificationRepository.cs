using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class TenderClassificationRepository : ITenderClassificationRepository
    {
        private readonly AuthContext _dbContext;

        public TenderClassificationRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<TenderClassification> ReadById(int id)
        {
            return await _dbContext.TenderClassification.FirstOrDefaultAsync(ad => ad.Id == id);
        }

        public async Task<TenderClassification> UpdateAsync(int id, TenderClassification TC)
        {
            var oldTC = await ReadById(id);
            if (oldTC == null) return null;
            oldTC.Description = TC.Description;
            oldTC.Name = TC.Name;
            oldTC.Amount= TC.Amount;

            var res = await _dbContext.SaveChangesAsync();
            return oldTC;
        }
    }
}
