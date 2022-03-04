using E_proc.Models;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {

        private readonly AuthContext _dbContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IDateService _dateService;



        public CitizenRepository(AuthContext dbContext, IEncryptionService encryptionService, IDateService dateService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
            _dateService = dateService;
        }


        //create citizen
        public async Task<Citizen> CreateAsync(Citizen citizen)
        {

            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == citizen.Email);
            if (foundedUser == null)
            {
                citizen.Password = _encryptionService.Encrypt(citizen.Password);

                Citizen x = new Citizen { Email = citizen.Email, FirstName = citizen.FirstName, LastName = citizen.LastName, Password = citizen.Password, Type = citizen.Type, CIN = citizen.CIN, Phone = citizen.Phone };

                var user = await _dbContext.Citizen.AddAsync(x);
                _dbContext.SaveChanges();

                return x;

            }
            else
            {
                return null;
            }

        }





        //get all citizens
        public async Task<IEnumerable<Citizen>> ReadAsync()
        {
            var citizens = await _dbContext.Citizen.ToListAsync();
            return citizens;
        }





        //get citizens by id

        public async Task<Citizen> ReadById(int id)
        {
            return await _dbContext.Citizen.FirstOrDefaultAsync(user => user.Id == id);
        }


        //update citizens
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
                    oldUser.CIN = user.CIN;
                    oldUser.Phone = user.Phone;
                    oldUser.Phone = user.Phone;
                    oldUser.updatedAt= new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
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



        //delete citizen
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

       
        //filter citizens by params
        public async Task<List<Citizen>> FindBy(string? email, bool? confirmed, string? cin, string? phone,DateTime? date)
        {
            var users = new List<Citizen>();
            
        
            long dateFromStamp = 0;
            long dateToStamp=0;
            //convert date to timestamp format 
            if (date.HasValue)
            {
                 dateFromStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0));
                 dateToStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59));
            }
         

       
            
            users = await _dbContext.Citizen
                 .Where(s => !string.IsNullOrEmpty(email) ? s.Email == email : true)
                 .Where(s => !string.IsNullOrEmpty(cin) ? s.CIN == cin : true)
                 .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                 .Where(s => confirmed.HasValue ? s.EmailConfirmed == confirmed : true)
                 .Where(s => date.HasValue  ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)
                 .ToListAsync();
          
            return users;

        }
    }
}
