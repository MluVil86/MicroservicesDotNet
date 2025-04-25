using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    internal class ProductService : IProduct
    {
        public Task<ProductResponse?> AddProduct(ProductAddRequest addProduct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid ProductID)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> GetProduct(Guid ProductID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponse>?> GetProductByCondition(string search)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponse>?> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> UpdateProduct(ProductUpdateRequest updateProduct)
        {
            throw new NotImplementedException();
        }
    }
}
