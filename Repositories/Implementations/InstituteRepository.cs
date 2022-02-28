using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class InstituteRepository : IInstituteRepository
    {
        private readonly AuthContext _dbContext;

        public InstituteRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<Institute> CreateAsync(Institute institute)
        {
            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == institute.Email);

            if (foundedUser == null)
            {
                Address adr = new Address { countryName = institute.address.countryName, city = institute.address.city, postalCode = institute.address.postalCode, street1 = institute.address.street1, street2 = institute.address.street2 };
                Representative representative = new Representative { Name=institute.Interlocutor.Name , Phone = institute.Interlocutor.Phone, Position = institute.Interlocutor.Position,SocialSecurityNumber=institute.Interlocutor.SocialSecurityNumber,SocialSecurityNumberDate=institute.Interlocutor.SocialSecurityNumberDate,Email=institute.Interlocutor.Email};
                Institute x = new Institute { Email = institute.Email , FirstName = institute.FirstName, LastName = institute.LastName, Password = institute.Password, Type = institute.Type,AreaType = institute.AreaType , Fax = institute.Fax,NameAr =institute.NameAr, NameFr=institute.NameFr,NotificationEmail=institute.NotificationEmail ,Phone = institute.Phone, representativeName= institute.representativeName,Interlocutor=representative,address=adr };
                var user = await _dbContext.Institute.AddAsync(x);
                _dbContext.SaveChanges();

                return x;

            }
            else
            {
                return null;
            }
        }

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

        public async Task<IEnumerable<Institute>> ReadAsync()
        {
        
            var institutes = await _dbContext.Institute.ToListAsync();
            return institutes;
        }


        public async Task<Institute> ReadById(int id)
        {
            return await _dbContext.Institute.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Institute> UpdateAsync(int id, Institute institute)
        {
            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == institute.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {


                 
                    oldUser.address.countryName = institute.address.countryName; oldUser.address.city = institute.address.city; oldUser.address.postalCode = institute.address.postalCode; oldUser.address.street1 = institute.address.street1; oldUser.address.street2 = institute.address.street2 ;
                    oldUser.Interlocutor.Name = institute.Interlocutor.Name; oldUser.Interlocutor.Phone = institute.Interlocutor.Phone; oldUser.Interlocutor.Position = institute.Interlocutor.Position; oldUser.Interlocutor.SocialSecurityNumber = institute.Interlocutor.SocialSecurityNumber; oldUser.Interlocutor.SocialSecurityNumberDate = institute.Interlocutor.SocialSecurityNumberDate; oldUser.Interlocutor.Email = institute.Interlocutor.Email ;
                    oldUser.Email = institute.Email; oldUser.FirstName = institute.FirstName; oldUser.LastName = institute.LastName; oldUser.Password = institute.Password; oldUser.Type = institute.Type; oldUser.AreaType = institute.AreaType; oldUser.Fax = institute.Fax;oldUser.NameAr = institute.NameAr; oldUser.NameFr = institute.NameFr; oldUser.NotificationEmail = institute.NotificationEmail; oldUser.Phone = institute.Phone; oldUser.representativeName = institute.representativeName ;


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
