using E_proc.Models;
using E_proc.Repositories.Interfaces;

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

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tender>> FindBy(string? email, bool? confirmed, string? phone, DateTime? date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tender>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tender> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tender> UpdateAsync(int id, Tender tender)
        {
            throw new NotImplementedException();
        }
    }
}
