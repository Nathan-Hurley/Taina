using System.Data.Entity;
using Taina.Data.Models;

namespace Taina.Data.Services
{
	public class TainaDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}
