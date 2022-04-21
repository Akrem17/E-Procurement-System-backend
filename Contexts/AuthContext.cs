using Microsoft.EntityFrameworkCore;
using E_proc.Models;

namespace E_proc.Models
{
    public class AuthContext:DbContext
    {

        public AuthContext() {
        

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("EprocDB"));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tender>()
                .HasOne(t => t.Institute)
                .WithMany(i => i.Tender)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FileData>()
                .HasOne(t => t.Tender).WithMany(t => t.Specifications).HasForeignKey(fk => fk.TenderId);

            if (false)
            {          
                modelBuilder.Entity<Institute>().Ignore(c => c.Tender);
            }
            modelBuilder.Entity<FileData>()
                .HasOne(f => f.Offer).WithMany(o => o.Files);
            modelBuilder.Entity<Tender>()
                    .HasMany(t=>t.Offers).WithOne(o=>o.Tender).HasForeignKey(f => f.TenderId).IsRequired();

            modelBuilder.Entity<OfferClassification>().HasOne(oc => oc.Offer).WithMany(o => o.OfferClassification).HasForeignKey(f => f.OfferId);

        }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }   

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UsersLogin { get; set; }
        public DbSet<E_proc.Models.Citizen> Citizen { get; set; }
        public DbSet<E_proc.Models.Supplier> Supplier { get; set; }
        public DbSet<E_proc.Models.Institute> Institute { get; set; }
        public DbSet<E_proc.Models.Tender> Tender { get; set; }
        public DbSet<E_proc.Models.Representative> Representative { get; set; }
        public DbSet<E_proc.Models.Address> Address { get; set; }
        public DbSet<E_proc.Models.TenderClassification> TenderClassification { get; set; }
        public DbSet<E_proc.Models.FileData> FileData { get; set; }
        public DbSet<E_proc.Models.Offer> Offer { get; set; }
        public DbSet<E_proc.Models.Licence> Licence { get; set; }
        public DbSet<E_proc.Models.Connections> Connection { get; set; }

        public DbSet<E_proc.Models.Notification> Notification { get; set; }
        public DbSet<E_proc.Models.OfferClassification> OfferClassification { get; set; }



    }
}
