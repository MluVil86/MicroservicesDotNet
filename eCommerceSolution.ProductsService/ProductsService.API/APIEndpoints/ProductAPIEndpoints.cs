using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductsService.BusinessLogicLayer.DTO;
using ProductsService.BusinessLogicLayer.ServiceContracts;
using ProductsService.BusinessLogicLayer.Services;
using ProductsService.BusinessLogicLayer.Validation;
using ProductsService.DataAccessLayer.Entities;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ProductsService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async(IProductService productService) =>
        {
            List<ProductResponse?> products = await productService.GetProducts();
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/productid/{ProductId:guid}", async (IProductService productService, Guid ProductId) =>
        {
            ProductResponse? products = await productService.GetProductByCondition(temp => temp.ProductID == ProductId);

            if (products == null)
                return Results.NotFound();

            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/{SearchString}", async (IProductService productService, string SearchString) =>
        {
            List<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(SearchString));            
            
            List<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(SearchString));

            var products = productsByProductName!.Union(productsByCategory!);
            return Results.Ok(products);

        });
        
        app.MapPost("/api/products/add", async (IProductService productService, IValidator<ProductAddRequest> productAddRequestValidator, 
            ProductAddRequest productAdd) =>
        {

            ValidationResult result = await productAddRequestValidator.ValidateAsync(productAdd);

            if (!result.IsValid)
            {
                Dictionary<string, string[]> errors =
                result.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());

                Results.ValidationProblem(errors);
            }

            ProductResponse? products = await productService.AddProduct(productAdd);

            if(products != null)
                return Results.Created(@$"/api/products/search/productid/{products.ProductID}", products);

            return Results.Problem("Error in adding product");
        });

        app.MapPut("/api/products/update", async (IProductService productService, IValidator<ProductUpdateRequest> productUpdateRequestValidator, 
            ProductUpdateRequest productUpdate) =>
        {
            ValidationResult result = await productUpdateRequestValidator.ValidateAsync(productUpdate);

            if (!result.IsValid)
            {
                Dictionary<string, string[]> errors =
                result.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());

                Results.ValidationProblem(errors);
            }


            ProductResponse? products = await productService.UpdateProduct(productUpdate);

            if (products != null)
                return Results.Ok(products);

            return Results.Problem("Error in updating product");
        });

        app.MapDelete("/api/products/delete/{ProductId:guid}", async (IProductService productService, Guid ProductId) =>
        {
            bool products = await productService.DeleteProduct(ProductId);

            if(products)
                return Results.Ok(products);
            return Results.Problem("Error in deleting product");
        });

        return app;
    }
}
