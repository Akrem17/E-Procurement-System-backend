using E_proc.Models;
using E_proc.Repositories.Interfaces;
using E_proc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly AuthContext _dbContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IDateService _dateService;

        public SupplierRepository(AuthContext dbContext, IEncryptionService encryptionService, IDateService dateService  )
        {

            _dbContext = dbContext;
            _encryptionService = encryptionService;
            _dateService=dateService;

        }




        //create supplier
        public async Task<Supplier> CreateAsync(Supplier supplier)

        {


            var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == supplier.Email);
            if (foundedUser == null)
            {
                
                supplier.Password = _encryptionService.Encrypt(supplier.Password);



                //Representative representative = new Representative
                //    {
                //        Name = supplier.representative.Name,
                //        Email = supplier.representative.Email,
                //        Phone = supplier.representative.Phone,
                //        Position = supplier.representative.Position,
                //        SocialSecurityNumber = supplier.representative.SocialSecurityNumber,
                //        SocialSecurityNumberDate = supplier.representative.SocialSecurityNumberDate

                //    };


                //Address address = new Address
                //{
                //    countryName = supplier.address.countryName,
                //    city = supplier.address.city,
                //    postalCode = supplier.address.postalCode,
                //    street1 = supplier.address.street1,
                //    street2 = supplier.address.street2
                //};
                //Licence licence = new Licence
                //{
                //    Category = supplier.licence.Category,
                //    AcquisitionDate = supplier.licence.AcquisitionDate,
                //    ExpirationDate = supplier.licence.ExpirationDate,
                //    IssuingInstitutionName = supplier.licence.IssuingInstitutionName,
                //    Name = supplier.licence.Name,
                //    RegistrationNumber = supplier.licence.RegistrationNumber

                //};

                //Supplier x = new Supplier
                //{
                //    Email = supplier.Email,
                //    FirstName = supplier.FirstName,
                //    LastName = supplier.LastName,
                //    Password = supplier.Password,
                //    Type = supplier.Type,
                //    CNSSId = supplier.CNSSId,
                //    Category = supplier.Category,
                //    BuisnessType = supplier.BuisnessType,
                //    BuisnessClassification = supplier.BuisnessClassification,
                //    address = address,
                //    CompanyName = supplier.CompanyName,
                //    Fax = supplier.Fax,
                //    licence = licence,
                //    Phone = supplier.Phone,
                //    RegistrationDate = supplier.RegistrationDate,
                //    RegistrationNumber = supplier.RegistrationNumber,
                //    ReplyEmail = supplier.ReplyEmail,
                //    representative = representative,
                //    SupplierName = supplier.SupplierName,
                //    TaxId = supplier.TaxId


           // };


            var user = await _dbContext.Supplier.AddAsync(supplier);
                _dbContext.SaveChanges();

                return supplier;

            }
            return null;

        }


        //delete supplier

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


        //filter suppliers using params 
        public async Task<List<Supplier>> FindBy(string? email, bool? confirmed, string? phone, DateTime? date)
        {
            var suppliers = new List<Supplier>();


            long dateFromStamp = 0;
            long dateToStamp = 0;
            //convert date to timestamp format 
            if (date.HasValue)
            {
                dateFromStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0));
                dateToStamp = _dateService.ConvertDatetimeToUnixTimeStamp(new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59));
            }

            suppliers = await _dbContext.Supplier
                 .Where(s => !string.IsNullOrEmpty(email) ? s.Email == email : true)
                 .Where(s => !string.IsNullOrEmpty(phone) ? s.Phone == phone : true)
                 .Where(s => confirmed.HasValue ? s.EmailConfirmed == confirmed : true)
                 .Where(s => date.HasValue ? Convert.ToInt64(s.createdAt) > dateFromStamp && Convert.ToInt64(s.createdAt) < dateToStamp : true)

                 .ToListAsync();





            return suppliers;
        }



        //get all suppliers
        public async Task<IEnumerable<Supplier>> ReadAsync()
        {
            var supplier = await _dbContext.Supplier.ToListAsync();
            return supplier;
        }



        //get supplier by id 
        public async Task<Supplier> ReadById(int id)
        {
            return await _dbContext.Supplier.FirstOrDefaultAsync(user => user.Id == id);
        }



        //update suppliers
        public async Task<Supplier> UpdateAsync(int id, Supplier supplier)
        {

            var oldUser = await ReadById(id);

            if (oldUser != null)
            {
                var foundedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == supplier.Email);

                if (foundedUser?.Id == id || foundedUser == null)
                {
                    supplier.Password = _encryptionService.Encrypt(supplier.Password);


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
