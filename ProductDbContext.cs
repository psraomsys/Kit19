using Kit19.Models;
using Microsoft.EntityFrameworkCore;

namespace Kit19
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }

    public class ProductSearchRepository
    {
        private readonly ProductDbContext _context;

        public ProductSearchRepository(ProductDbContext context)
        {
            _context = context;
        }
    }
}
