using E_proc.Models;
using E_proc.Repositories;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Services
{
    public class UserService:IUserService
    {

         
        private readonly AuthContext _dbContext;
        private readonly ICitizenRepository _reposCitizen;
        private readonly IEncryptionService _encryptionService;


        public UserService(AuthContext dbContext, ICitizenRepository reposCitizen, IEncryptionService encryptionService)
        {
            _dbContext = dbContext;
            _reposCitizen=reposCitizen;
            _encryptionService=encryptionService;



        }
        public async Task<User> GetByEmailAndPassword(UserLogin userLogin)
        {
        

            userLogin.Password = _encryptionService.Encrypt(userLogin.Password);
            Console.WriteLine(userLogin.Password);
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(userLogin.Email) && u.Password == userLogin.Password);
            
         
        }


        public async Task<int> Signup(User user)
        {

            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (foundedUser == null)
            {
                Console.WriteLine(user.Type);
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

        public async Task<Citizen> SignupCitizen(Citizen user)
        {

            var foundedUser = await _reposCitizen.CreateAsync(user);

            return foundedUser;
            }

        }
    }

