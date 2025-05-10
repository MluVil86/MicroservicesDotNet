using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using ProductsService.BusinessLogicLayer.DTO;
using ProductsService.BusinessLogicLayer.ServiceContracts;
using ProductsService.DataAccessLayer.Entities;
using ProductsService.DataAccessLayer.RepositoryContracts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProductsService.BusinessLogicLayer.Services;

public class ProductService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, 
        IMapper mapper, IProductRepository productRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
            throw new ArgumentNullException(nameof(productAddRequest));

        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));

            throw new ArgumentException(errors);
        }

        Product productInput = _mapper.Map<Product>(productAddRequest);

        Product? addedProduct = await _productRepository.AddProduct(productInput);

        if(addedProduct == null)
            return null;

        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);

        return addedProductResponse;
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdate)
    {

        Product? existingProduct = await _productRepository.GetProductByCondition(temp => temp.ProductID == productUpdate.ProductID);

        if (existingProduct == null)
            throw new ArgumentException("Invalid Product ID");

        
        ValidationResult validateResult = await _productUpdateRequestValidator.ValidateAsync(productUpdate);

        if (!validateResult.IsValid)
        {
            string errors = string.Join(", ", validateResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product productInput = _mapper.Map<Product>(productUpdate);

        Product? updatedProduct = await _productRepository.UpdateProduct(productInput);

        if (updatedProduct == null) 
            return null;

        ProductResponse updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);

        return updatedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid ProductID)
    {
        Product? existingProduct = await _productRepository.GetProductByCondition(temp => temp.ProductID == ProductID);

        if (existingProduct == null)
            return false;

        return  await _productRepository.DeleteProduct(ProductID);        
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {

        Product? getProduct = await _productRepository.GetProductByCondition(conditionExpression);

        if (getProduct == null)
            return null;

        ProductResponse getProductResponse = _mapper.Map<ProductResponse>(getProduct);

        return getProductResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> getProducts = await _productRepository.GetProducts();

        if (getProducts == null)
            return null;

        IEnumerable<ProductResponse?> getProductsResponse = _mapper.Map<IEnumerable<ProductResponse>>(getProducts);

        return getProductsResponse.ToList();


    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        IEnumerable<Product?> getProducts = await _productRepository.GetProductsByCondition(conditionExpression);       

        IEnumerable<ProductResponse?> getProductsResponse = _mapper.Map<IEnumerable<ProductResponse>>(getProducts);

        return getProductsResponse.ToList();
    }

    
}
