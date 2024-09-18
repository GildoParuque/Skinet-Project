using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace SkinetAPI.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
    {
        
    }

    public DbSet<Product> Products { get; set; }
}
