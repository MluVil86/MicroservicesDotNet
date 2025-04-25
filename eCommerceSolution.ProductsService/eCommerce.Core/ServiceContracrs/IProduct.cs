using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.ServiceContracrs;

public interface IProduct
{
    Task<ProductResponse?> AddProduct(ProductAddRequest addProduct);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest updateProduct);
    Task<List<ProductResponse>?> GetProducts();
    Task<List<ProductResponse>?> GetProductByCondition(string search);
    Task<ProductResponse?> GetProduct(Guid ProductID);
    Task DeleteProduct(Guid ProductID);
}





