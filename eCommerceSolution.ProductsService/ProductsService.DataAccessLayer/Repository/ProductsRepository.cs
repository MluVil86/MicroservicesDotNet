using Microsoft.EntityFrameworkCore;
using ProductsService.DataAccessLayer.Context;
using ProductsService.DataAccessLayer.Entities;
using ProductsService.DataAccessLayer.RepositoryContracts;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace ProductsService.DataAccessLayer.Repository;

public class ProductsRepository : IProductRepository
{
    public readonly ProductDbContext _context;
    public ProductsRepository(ProductDbContext context)
    {
        _context = context; 
    }
    public async Task<Product?> AddProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProduct(Guid ProductId)
    {
        Product? existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == ProductId); 

        if (existingProduct == null) 
            return false;

        _context.Products.Remove(existingProduct!);
        int affectedRowsCount = await _context.SaveChangesAsync();  

        return affectedRowsCount > 0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _context.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _context.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    

    public async Task<Product?> UpdateProduct(Product product)
    {
        Product? existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

        if (existingProduct == null)
            return null;

        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category =  product.Category;

        await _context.SaveChangesAsync();

        return existingProduct;
    }
}
