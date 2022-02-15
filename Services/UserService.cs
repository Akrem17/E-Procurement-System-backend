using E_proc.Models;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Services
{
    public class UserService:IUserService
    {

         
        private readonly AuthContext _dbContext;


        public UserService(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetByEmailAndPassword(UserLogin userLogin)
        {
        

            return await _dbContext.Users.FirstOrDefaultAsync(u =>u.Email.Equals(userLogin.Email) && u.Password == userLogin.Password   );
            
         
        }


        public async Task<int> Signup(User user)
        {
            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (foundedUser == null)
            {
                User x = new User { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password = user.Password, Type = user.Type };

                await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();
                return 200;

            }
            else
            {
                return 409;
            }


        }

    }
}
