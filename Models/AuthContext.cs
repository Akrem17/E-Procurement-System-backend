using Microsoft.EntityFrameworkCore;

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
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }   

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UsersLogin { get; set; }
    }
}
