using E_proc.Models;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _dbContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IDateService _dateService;

        public UserRepository(AuthContext dbContext, IEncryptionService encryptionService , IDateService dateService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
            _dateService = dateService;
        }




        //add user
        public async Task<int> CreateAsync(User user)
        {
            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (foundedUser == null)
            {
                User x = new User { Email = user.Email, Password = user.Password, Type = user.Type };

                await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();
                return 200;

            }
            else
            {
                return 409;
            }


        }

        //delete user
        public async Task<int> Delete(int id)
        {

            var user = await Read(id);
            if (user != null)
            {
                var deletedUser = _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;

        }


        //
        public async Task<IEnumerable<User>> ReadAsync()
        {

            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> Read(int id)
        {



            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        }



        public async Task<User> UpdateAsync(int id, User user)
        {
            var oldUser = await Read(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {


                    oldUser.Email = user.Email;

                   
                    oldUser.Password = _encryptionService.Encrypt(user.Password);
                    oldUser.Type = user.Type;
                    oldUser.updatedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
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



        

        public async Task<User> ResetPasswordAsync(User user, string token, string newPassword)
        {

            user.Password = _encryptionService.Encrypt(newPassword);
            user.updatedAt  = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            var updated = _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async  Task<List<User>> FindBy(string? email=null, bool? confirmed = null, DateTime? date = null)
        {
            var users = new List<User>();

            long dateFromStamp = 0;
            long dateToStamp = 0;
            //convert date to timestamp format 
            if (date.HasValue)
            {
                dateFromStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0));
                dateToStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59));
            }

            users = await _dbContext.Users
                 .Where(s => !string.IsNullOrEmpty(email) ? s.Email == email : true)
                 .Where(s => confirmed.HasValue ? s.EmailConfirmed == confirmed : true)
                 .Where(s => date.HasValue ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)

                 .ToListAsync();


            return users;
        }
    }
}
