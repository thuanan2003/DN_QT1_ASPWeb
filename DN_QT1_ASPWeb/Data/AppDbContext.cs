using Microsoft.EntityFrameworkCore;
using DN_QT1_ASPWeb.Model;
namespace DN_QT1_ASPWeb.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Item> Items { get; set; }
	}
}
