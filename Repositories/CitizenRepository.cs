using E_proc.Models;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {

        private readonly AuthContext _dbContext;


        public CitizenRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Citizen> CreateAsync(Citizen citizen)
        {

            var foundedUser = await _dbContext.Citizen.FirstOrDefaultAsync(u => u.Email == citizen.Email);
            if (foundedUser == null)
            {
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
                var foundedUser = await _dbContext.Citizen.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {


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

    }
}
