using eCommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.RepositoryContracts;

public interface IProductRepository
{

    Task<List<Product>?> GetProducts();
    Task<Product?> GetProductByCondition(string Product);
    Task<Product?> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task DeleteProduct(Guid ProductID);
}
