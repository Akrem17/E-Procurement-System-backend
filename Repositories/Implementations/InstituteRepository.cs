using E_proc.Models;
using E_proc.Repositories.Interfaces;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class InstituteRepository : IInstituteRepository
    {
        private readonly AuthContext _dbContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IDateService _dateService;
        public InstituteRepository(AuthContext dbContext, IEncryptionService encryptionService, IDateService dateService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
            _dateService = dateService;
        }


        //create insitute
        public async Task<Institute> CreateAsync(Institute institute)
        {
            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == institute.Email);

            if (foundedUser == null)
            {
                institute.Password = _encryptionService.Encrypt(institute.Password);
                Address adr = new Address { countryName = institute.address.countryName, city = institute.address.city, postalCode = institute.address.postalCode, street1 = institute.address.street1, street2 = institute.address.street2 };
                Representative representative = new Representative { Name = institute.Interlocutor.Name, Phone = institute.Interlocutor.Phone, Position = institute.Interlocutor.Position, SocialSecurityNumber = institute.Interlocutor.SocialSecurityNumber, SocialSecurityNumberDate = institute.Interlocutor.SocialSecurityNumberDate, Email = institute.Interlocutor.Email };
                Institute x = new Institute { Email = institute.Email, Password = institute.Password, Type = institute.Type, AreaType = institute.AreaType, Fax = institute.Fax, NameAr = institute.NameAr, NameFr = institute.NameFr, NotificationEmail = institute.NotificationEmail, Phone = institute.Phone, representativeName = institute.representativeName, Interlocutor = representative, address = adr };

                var user = await _dbContext.Institute.AddAsync(x);
                _dbContext.SaveChanges();

                return x;

            }
            else
            {
                return null;
            }
        }




        //delete insitute
        public async Task<int> Delete(int id)
        {

            var user = await ReadById(id);
            if (user != null)
            {
                var deletedUser = _dbContext.Institute.Remove(user);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;


        }



        //filter insitutes by params
        public async Task<List<Institute>> FindBy(string? email, bool? confirmed, string? phone,DateTime? date)
        {
            var institutes = new List<Institute>();


            long dateFromStamp = 0;
            long dateToStamp = 0;
            //convert date to timestamp format 
            if (date.HasValue)
            {
                dateFromStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0));
                dateToStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59));
            }

            institutes = await _dbContext.Institute
                     .Where(s => !string.IsNullOrEmpty(email) ? s.Email == email : true)
                     .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                     .Where(s => confirmed.HasValue ? s.EmailConfirmed == confirmed : true)
                     .Where(s => date.HasValue ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)
                     .ToListAsync();



            return institutes;
        }



        //get all insitutes
        public async Task<IEnumerable<Institute>> ReadAsync()
        {

            var institutes = await _dbContext.Institute.ToListAsync();

            return institutes;
        }




        //get insitute by id
        public async Task<Institute> ReadById(int id)
        {
            return await _dbContext.Institute.FirstOrDefaultAsync(user => user.Id == id);
        }




        //update institute
        public async Task<Institute> UpdateAsync(int id, Institute institute)
        {
            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == institute.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {

                    institute.Password = _encryptionService.Encrypt(institute.Password);

                    oldUser.address.countryName = institute.address.countryName; oldUser.address.city = institute.address.city; oldUser.address.postalCode = institute.address.postalCode; oldUser.address.street1 = institute.address.street1; oldUser.address.street2 = institute.address.street2;
                    oldUser.Interlocutor.Name = institute.Interlocutor.Name; oldUser.Interlocutor.Phone = institute.Interlocutor.Phone; oldUser.Interlocutor.Position = institute.Interlocutor.Position; oldUser.Interlocutor.SocialSecurityNumber = institute.Interlocutor.SocialSecurityNumber; oldUser.Interlocutor.SocialSecurityNumberDate = institute.Interlocutor.SocialSecurityNumberDate; oldUser.Interlocutor.Email = institute.Interlocutor.Email;
                    oldUser.Email = institute.Email;  oldUser.Password = institute.Password; oldUser.Type = institute.Type; oldUser.AreaType = institute.AreaType; oldUser.Fax = institute.Fax; oldUser.NameAr = institute.NameAr; oldUser.NameFr = institute.NameFr; oldUser.NotificationEmail = institute.NotificationEmail; oldUser.Phone = institute.Phone; oldUser.representativeName = institute.representativeName;
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
    }
}
