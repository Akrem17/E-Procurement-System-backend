using E_proc.Models;

namespace E_proc.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> UpdateAsync(int id, Address address);
        Task<Address> ReadById(int id);


    }
}
