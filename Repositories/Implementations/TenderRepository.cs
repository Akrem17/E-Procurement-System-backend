using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class TenderRepository : ITenderRepository
    {

        private readonly AuthContext _dbContext;

        public TenderRepository(AuthContext dbContext)
        {
            _dbContext = dbContext; 
                
        }
        public async Task<Tender> CreateAsync(Tender tender)
        {

            var tenderResult = await _dbContext.Tender.AddAsync(tender);
            _dbContext.SaveChanges();

            return tender;

        }

        public async Task<int> Delete(int id)
        {

            var tender = await ReadById(id);
            if (tender != null)
            {
                var deletedUser = _dbContext.Tender.Remove(tender);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;

        }

        public Task<List<Tender>> FindBy(string? email, bool? confirmed, string? phone, DateTime? date)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tender>> ReadAsync()
        {
            var tender = await _dbContext.Tender.ToListAsync();
            return tender;
        }

        public async Task<Tender> ReadById(int id)
        {

            return await _dbContext.Tender.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Tender> UpdateAsync(int id, Tender tender)
        {
            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                
                oldUser.StartDate = tender.StartDate;oldUser.GuaranteeType = tender.GuaranteeType;oldUser.Financing = tender.Financing;oldUser.Budget=tender.Budget;oldUser.BusinessKind=tender.BusinessKind;oldUser.Departement=tender.Departement;oldUser.EvaluationMethod=tender.EvaluationMethod;oldUser.Name = tender.Name; oldUser.specificationURL = tender.specificationURL;
                oldUser.updatedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

                    await _dbContext.SaveChangesAsync();
                    return oldUser;
                }
              
                    return null;

                
            }
          
      
    }
}
