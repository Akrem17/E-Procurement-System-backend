using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly AuthContext _dbContext;
        public SupplierRepository( AuthContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<Supplier> CreateAsync(Supplier supplier)

        {

            Console.WriteLine("hii");
            var foundedUser = await _dbContext.Supplier.FirstOrDefaultAsync(u => u.Email == supplier.Email);
            if (foundedUser == null)
            {
                Representative representative = new Representative { 
                    Name = supplier.representative.Name,
                    Email = supplier.representative.Email,  
                    Phone = supplier.representative.Phone,  
                    Position = supplier.representative.Position,    
                    SocialSecurityNumber = supplier.representative.SocialSecurityNumber,
                    SocialSecurityNumberDate = supplier.representative.SocialSecurityNumberDate             
                
                };
                Address address = new Address
                {
                    countryName = supplier.address.countryName,
                    city = supplier.address.city,
                    postalCode = supplier.address.postalCode,
                    street1 = supplier.address.street1,
                    street2 = supplier.address.street2
                };
                Licence licence = new Licence
                {
                    Category = supplier.licence.Category,
                    AcquisitionDate = supplier.licence.AcquisitionDate,
                    ExpirationDate = supplier.licence.ExpirationDate,
                    IssuingInstitutionName = supplier.licence.IssuingInstitutionName,
                    Name = supplier.licence.Name,
                    RegistrationNumber = supplier.licence.RegistrationNumber

                };

                Supplier x= new Supplier {
                    Email = supplier.Email,
                    FirstName = supplier.FirstName, 
                    LastName = supplier.LastName, 
                    Password = supplier.Password, 
                    Type = supplier.Type,
                    CNSSId = supplier.CNSSId,
                    Category = supplier.Category,
                    BuisnessType = supplier.BuisnessType,
                    BuisnessClassification = supplier.BuisnessClassification,
                    address = address,
                    CompanyName = supplier.CompanyName,
                    Fax = supplier.Fax,
                    licence = licence,
                    Phone = supplier.Phone,
                    RegistrationDate = supplier.RegistrationDate,   
                    RegistrationNumber  = supplier.RegistrationNumber,
                    ReplyEmail = supplier.ReplyEmail,
                    representative = representative,
                    SupplierName = supplier.SupplierName,
                    TaxId = supplier.TaxId
                    
                
                };
            
                var user = await _dbContext.Supplier.AddAsync(x);
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
                var deletedUser = _dbContext.Supplier.Remove(user);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;

        

    }

        public async Task<IEnumerable<Supplier>> ReadAsync()
        {
            var supplier = await _dbContext.Supplier.ToListAsync();
            return supplier;
        }

        public async Task<Supplier> ReadById(int id)
        {
            return await _dbContext.Supplier.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Supplier> UpdateAsync(int id, Supplier supplier)
        {

            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Supplier.FirstOrDefaultAsync(u => u.Email == supplier.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {

                    oldUser.licence.Category = supplier.licence.Category;
                    oldUser.licence.AcquisitionDate = supplier.licence.AcquisitionDate;
                    oldUser.licence.ExpirationDate = supplier.licence.ExpirationDate;
                    oldUser.licence.IssuingInstitutionName = supplier.licence.IssuingInstitutionName;
                    oldUser.licence.Name = supplier.licence.Name;

                    oldUser.representative.Name = supplier.representative.Name;
                    oldUser.representative.Email = supplier.representative.Email;
                    oldUser.representative.Phone = supplier.representative.Phone;
                    oldUser.representative.Position = supplier.representative.Position;
                    oldUser.representative.SocialSecurityNumber = supplier.representative.SocialSecurityNumber;
                    oldUser.representative.SocialSecurityNumberDate = supplier.representative.SocialSecurityNumberDate;
                    oldUser.RegistrationNumber = supplier.licence.RegistrationNumber;

                    oldUser.address.countryName = supplier.address.countryName;
                     oldUser.address.city = supplier.address.city;
                     oldUser.address.postalCode = supplier.address.postalCode;
                     oldUser.address.street1 = supplier.address.street1;
                    oldUser.address.street2 = supplier.address.street2;

                    oldUser.Email = supplier.Email;
                    oldUser.FirstName = supplier.FirstName;
                    oldUser.LastName = supplier.LastName;
                    oldUser.Password = supplier.Password;
                    oldUser.Type = supplier.Type;
                    oldUser.CNSSId = supplier.CNSSId;
                    oldUser.Category = supplier.Category;
                    oldUser.BuisnessType = supplier.BuisnessType;
                    oldUser.BuisnessClassification = supplier.BuisnessClassification;
                    oldUser.CompanyName = supplier.CompanyName;
                    oldUser.Fax = supplier.Fax;
                    oldUser.Phone = supplier.Phone;
                    oldUser.RegistrationDate = supplier.RegistrationDate;
                    oldUser.RegistrationNumber = supplier.RegistrationNumber;
                    oldUser.ReplyEmail = supplier.ReplyEmail;
                    oldUser.SupplierName = supplier.SupplierName;
                    oldUser.TaxId = supplier.TaxId;

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
