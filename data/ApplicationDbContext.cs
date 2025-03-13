using Microsoft.EntityFrameworkCore;
using POService.Models;

namespace POService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
