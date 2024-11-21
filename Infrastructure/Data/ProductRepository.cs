using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using SkinetAPI.Data;
namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(c => c.ProductBrand)
            .Include(c => c.ProductType)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(c => c.ProductBrand)
            .Include(c => c.ProductType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
}
