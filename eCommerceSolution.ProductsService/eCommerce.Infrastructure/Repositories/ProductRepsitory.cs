using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories;

public class ProductRepsitory : IProductRepository
{
    public Task<Product?> AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(Guid ProductID)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByCondition(string Product)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>?> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
