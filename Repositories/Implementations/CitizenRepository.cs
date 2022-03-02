using E_proc.Models;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {

        private readonly AuthContext _dbContext;
        private readonly IEncryptionService _encryptionService;


        public CitizenRepository(AuthContext dbContext, IEncryptionService encryptionService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
        }
        public async Task<Citizen> CreateAsync(Citizen citizen)
        {

            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == citizen.Email);
            if (foundedUser == null)
            {
                citizen.Password= _encryptionService.Encrypt(citizen.Password);
               
                Citizen x = new Citizen { Email = citizen.Email, FirstName = citizen.FirstName, LastName = citizen.LastName, Password = citizen.Password, Type = citizen.Type,CIN= citizen.CIN,Phone=citizen.Phone };

               var user= await _dbContext.Citizen.AddAsync(x);
                _dbContext.SaveChanges();
                
                return  x ;

            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<Citizen>> ReadAsync()
        {
            var citizens = await _dbContext.Citizen.ToListAsync();
            return citizens;
        }


        public async Task<Citizen> ReadById(int id)
        {
            return await _dbContext.Citizen.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Citizen> UpdateAsync(int id, Citizen user)
        {
            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);


                if (foundedUser?.Id == id || foundedUser == null)
                {
                    user.Password = _encryptionService.Encrypt(user.Password);


                    oldUser.Email = user.Email;
                    oldUser.FirstName = user.FirstName;
                    oldUser.LastName = user.LastName;
                    oldUser.Password = user.Password;
                    oldUser.Type = user.Type;
                    oldUser.CIN=user.CIN;
                    oldUser.Phone=user.Phone;

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


        public async Task<int> Delete(int id)
        {

            var user = await ReadById(id);
            if (user != null)
            {
                var deletedUser = _dbContext.Citizen.Remove(user);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;

        }

        public async Task<List<Citizen>> FindBy(string? email, bool? confirmed,string? cin, string? phone)
        {
            var users = new List<Citizen>();

            //var query = from s in _dbContext.Citizen
            //            where EF.Functions.Like(s.Email, email)
            //            where EF.Functions.Like(s.Title, "%angel%")
            //            where EF.Functions.Like(s.Title, "%angel%")
            //            select s;


            users = await _dbContext.Citizen
                 .Where(s =>  !string.IsNullOrEmpty(email) ? s.Email == email : true)
                    

                 .Where(s => !string.IsNullOrEmpty(cin)  ? s.CIN == cin : true)
                 .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                 .Where(s => confirmed.HasValue ? s.EmailConfirmed == confirmed : true)

                 .ToListAsync();

            return users;

        }
    }
}
