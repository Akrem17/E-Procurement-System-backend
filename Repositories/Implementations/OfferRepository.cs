using E_proc.Models;
using E_proc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_proc.Repositories.Implementations
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AuthContext _dbContext;
        private readonly IFileDataRepository _fileDataRepository;

        public OfferRepository(AuthContext dbContext, IFileDataRepository fileDataRepository )
        {

            _dbContext = dbContext;
            _fileDataRepository = fileDataRepository;
        }

        public int CountData()
        {
            return _dbContext.Offer.Count();
        }
      

        public async Task<Offer> CreateAsync(Offer offer)
        {
            Offer off = new Offer();
            offer.Id = 0;
             off = offer.Copy();
                 var of = await _dbContext.Offer.AddAsync(off);
                _dbContext.SaveChanges();
                 Console.WriteLine(off);
                 Console.WriteLine(offer);

            return off;




        }

        public async Task<int> Delete(int id)
        {


            var offer = await ReadById(id);
            ICollection<FileData> files=offer.Files;

            if (offer != null)
            {
                foreach (FileData file in files)
                {
                  var res=  _fileDataRepository.deleteFile(file.Id);
                    
                }
                var deletedOffer = _dbContext.Offer.Remove(offer);
                await _dbContext.SaveChangesAsync();
                return 200;
            };
            return 404;
        }

        public async Task<List<Offer>> FindBy(int skip, int take,string? supplierId, string? supplierEmail, string? offerNumber = null, string? tenderName = null, string? city = null, string? postDate = null)
        {
            var offers = await _dbContext.Offer
                                .Where(s => !string.IsNullOrEmpty(supplierId) ? s.SupplierId.ToString() == supplierId : true)
                                .Where(s => !string.IsNullOrEmpty(supplierEmail) ? s.Supplier.Email == supplierEmail : true)
                                .Where(s => !string.IsNullOrEmpty(offerNumber) ? EF.Functions.Like(s.Id.ToString(), offerNumber + "%") : true)
                                .Where(s => !string.IsNullOrEmpty(tenderName) ? EF.Functions.Like(s.Name, tenderName + "%") : true)
                               
                            

                                 .Include(t => t.Supplier).Skip(skip).Take(take)
                                .ToListAsync();
            Tender tender;
            for (var i = 0; i < offers.Count; i++)
            {
                tender = await _dbContext.Tender.Where(t => t.Id == offers[i].TenderId).FirstOrDefaultAsync();
                tender.Offers = null;
                offers[i].TenderInfo = tender;
            }


            return offers;
        }
        public int CountDataWithFilters(int skip, int take, string? supplierId, string? supplierEmail, string? offerNumber = null, string? tenderName = null, string? city = null, string? postDate = null)
        {
            return _dbContext.Offer
                            .Where(s => !string.IsNullOrEmpty(supplierId) ? s.SupplierId.ToString() == supplierId : true)
                            .Where(s => !string.IsNullOrEmpty(supplierEmail) ? s.Supplier.Email == supplierEmail : true)
                            .Where(s => !string.IsNullOrEmpty(offerNumber) ? EF.Functions.Like(s.Id.ToString(), offerNumber + "%") : true)
                            .Where(s => !string.IsNullOrEmpty(tenderName) ? EF.Functions.Like(s.Name, tenderName + "%") : true).Count(); 



                                 
                

        }


        public async Task<IEnumerable<Offer>> ReadAsync(int skip, int take)
        {
            var offers = await _dbContext.Offer.Include(o => o.Supplier).Include(o => o.Tender).Include(o => o.Files).Skip(skip).Take(take).ToArrayAsync();



            return offers;
        }

        public async Task<Offer> ReadById(int id)
        {
            return await _dbContext.Offer.Include(o => o.Files).Include(o => o.Supplier).ThenInclude(s => s.address).FirstOrDefaultAsync(ad => ad.Id == id);

        }

        public async Task<Offer> UpdateAsync(int id, Offer offer)
        {

            var oldOffer = await ReadById(id);

            if (oldOffer != null)
            {
                oldOffer.Name = offer.Name;oldOffer.TotalMontant = offer.TotalMontant;offer.FinalTotalMontant = offer.FinalTotalMontant;offer.isAccepted = offer.isAccepted; offer.SupplierId = offer.SupplierId;
                
                await _dbContext.SaveChangesAsync();
                return oldOffer;
            }

            return null;


        }
    }
    }

