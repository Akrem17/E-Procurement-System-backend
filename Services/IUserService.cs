using E_proc.Models;

namespace E_proc.Services
{
    public interface IUserService
    {

        public Task<User> GetByEmailAndPassword(UserLogin userLogin);
        public Task<int> Signup(User user);  
        
    }
}
