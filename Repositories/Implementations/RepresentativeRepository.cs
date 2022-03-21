using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class RepresentativeRepository : IRepresentativeRepository
    {
        private readonly AuthContext _dbContext;
        public RepresentativeRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;

        }
        public Task<Representative> CreateAsync(Representative representative)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Representative>> FindBy(string? socialSecurityNumber)
        {
            var representatives = new List<Representative>();
            representatives = await _dbContext.Representative
                   .Where(s => !string.IsNullOrEmpty(socialSecurityNumber) ? s.SocialSecurityNumber == socialSecurityNumber : true)
                   .ToListAsync();
            return representatives;

        }

        public async Task<IEnumerable<Representative>> ReadAsync()
        {
            var representative = await _dbContext.Representative.ToListAsync();

            return representative;
        }

        public async Task<Representative> ReadById(int id)
        {
            return await _dbContext.Representative.FirstOrDefaultAsync(ad => ad.Id == id);
        }

        public async Task<Representative> UpdateAsync(int id, Representative representative)
        {
            var oldRepresentative = await ReadById(id);
            if (oldRepresentative == null) return null;
            oldRepresentative.Email = representative.Email;
            oldRepresentative.Phone = representative.Phone;
            oldRepresentative.Position = representative.Position;
            oldRepresentative.Name = representative.Name; 
            oldRepresentative.SocialSecurityNumber = representative.SocialSecurityNumber;
            oldRepresentative.SocialSecurityNumberDate = representative.SocialSecurityNumberDate;

            var res = await _dbContext.SaveChangesAsync();

            return oldRepresentative;
        }
    }
}
