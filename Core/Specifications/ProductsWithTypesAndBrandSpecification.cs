using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
