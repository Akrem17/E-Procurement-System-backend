using E_proc.Models;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _dbContext;


        public UserRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(User user)
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
     

        public async Task<int> Delete(int id)
        {

            var user = await Read(id);
            if (user != null) {
                var deletedUser = _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return 200;
            };          
             return  404;

        }

        public async Task<IEnumerable<User>> ReadAsync()
        {

            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> Read(int id)
        {
       
            

              return  await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id );
            
        }



        public async Task<User> UpdateAsync(int id, User user)
        {
            var oldUser = await Read(id);
          
                if (oldUser != null)
            {
                var  foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                
                if (foundedUser?.Id == id || foundedUser == null)
                {

       
                oldUser.Email = user.Email; 
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;   
                oldUser.Password = user.Password;   
                oldUser.Type=user.Type;
               
                await _dbContext.SaveChangesAsync();
                    return oldUser;
                }
                else
                {
                    return null;

                }
            };
            return oldUser;
        }

       

        public async Task<List<User>> FindBy(string? email=null, Nullable<bool> confirmed =null)
        {

            var users = new List<User>();

            if (email == null && confirmed == null)
            {
                 users = await _dbContext.Users.Where(u => u.Email == email).ToListAsync();
              
            }
            else if (email != null && confirmed == null)
            {
                 users = await _dbContext.Users.Where(u => u.Email == email).ToListAsync();
              
            }
            else if (email == null && confirmed != null)
            {
                 users = await _dbContext.Users.Where(u => u.EmailConfirmed == confirmed).ToListAsync();
               
            }

            return users;



        }
    }
}
