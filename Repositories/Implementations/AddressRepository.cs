using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AuthContext _dbContext;


        public AddressRepository(AuthContext dbContext)
        {
                
            _dbContext = dbContext;
        }

        public async Task<Address> ReadById(int id)
        {
            return await _dbContext.Address.FirstOrDefaultAsync(ad => ad.Id == id);
        }

        public async Task<Address> UpdateAsync(int id, Address address)
        {
            var oldAddress = await ReadById(id);
            if (oldAddress == null) return null;    
            oldAddress.postalCode=address.postalCode;
            oldAddress.street1=address.street1;
           
            oldAddress.city=address.city;
            var res=await _dbContext.SaveChangesAsync();
            return oldAddress;                         

        }
    }
}
