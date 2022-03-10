using Microsoft.EntityFrameworkCore;
using E_proc.Models;

namespace E_proc.Models
{
    public class AuthContext:DbContext
    {

        public AuthContext() { }

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
                .WithMany(i => i.Tender).
                OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Tender>().Ignore(c => c.Institute);



        }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }   

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UsersLogin { get; set; }
        public DbSet<E_proc.Models.Citizen> Citizen { get; set; }
        public DbSet<E_proc.Models.Supplier> Supplier { get; set; }
        public DbSet<E_proc.Models.Institute> Institute { get; set; }
        public DbSet<E_proc.Models.Tender> Tender { get; set; }


    }
}
