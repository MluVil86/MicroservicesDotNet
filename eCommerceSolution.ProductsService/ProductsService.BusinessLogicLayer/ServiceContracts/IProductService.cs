using ProductsService.BusinessLogicLayer.DTO;
using ProductsService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductsService.BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    Task<List<ProductResponse?>> GetProducts();
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);    
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdate);
    Task<bool> DeleteProduct(Guid ProductID);
}
