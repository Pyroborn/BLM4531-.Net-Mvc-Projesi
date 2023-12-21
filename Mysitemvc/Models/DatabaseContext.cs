using Microsoft.EntityFrameworkCore;

namespace Mysitemvc.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Usermodel> Users { get; set; }
    }
}
