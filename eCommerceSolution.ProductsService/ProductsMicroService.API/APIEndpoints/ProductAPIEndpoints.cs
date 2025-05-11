using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Runtime.CompilerServices;

namespace ProductsMicroService.API.APIEndpoints
{
    public static class ProductAPIEndpoints
    {
        public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
        {
            // GET  /api/products
            app.MapGet("/api/products", async (IProductsService productsService) =>
            {
               List<ProductResponse?> products = await productsService.GetProducts();
                return Results.Ok(products);
            });
            
            //Get /api/products/search/product-id/{ProductID}
            app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
            {
                ProductResponse? product = await productsService.GetProductByCondition(temp => temp.ProductID == ProductID);
                if(product == null)
                {
                    return Results.NotFound($"Product with ID {ProductID} not found");
                }
                return Results.Ok(product);
            });

            //Get /api/products/search/xxxxxx
            app.MapGet("/api/products/search/{SearchString}", async (IProductsService productsService, String SearchString) =>
            {
               List<ProductResponse?> productsByProductName = await productsService.GetProductsByCondition(temp=>  temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
                List<ProductResponse?> productsByCategory = await productsService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
                var products = productsByProductName.Union(productsByCategory);

                return Results.Ok(products);
            });

            //POST /api/products
            app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> productAddRequestValidator, ProductAddRequest productAddRequest) =>
            {
                //Validate the productAddRequest object using Fluent Validation
                ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

                //check the validation result
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                      .GroupBy(temp => temp.PropertyName)
                      .ToDictionary(grp => grp.Key,
                        grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                var addedProductResponse = await productsService.AddProduct(productAddRequest);
                if (addedProductResponse != null)
                    return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductID}", addedProductResponse);
                else
                    return Results.Problem("Error in adding product");
            });

            //PUT /api/products
            app.MapPut("/api/products", async (IProductsService productsService, IValidator<ProductUpdateRequest> productUpdateRequestValidator, ProductUpdateRequest productUpdateRequest) =>
            {
                //Validate the ProductUpdateRequest
                ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
                //Check the validation result
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                      .GroupBy(temp => temp.PropertyName)
                      .ToDictionary(grp => grp.Key,
                        grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }

                var updatedProductResponse = await productsService.UpdateProduct(productUpdateRequest);
                if(updatedProductResponse != null)
                {
                    return Results.Ok(updatedProductResponse);
                }
                else
                {
                    return Results.Problem("Error in updating product");
                }
            });

            //DELETE /api/products/xxxxxxxxxxx
            app.MapDelete("/api/products/{ProductID:guid}", async (IProductsService productService, Guid ProductID) =>
            {
                bool isDeleted = await productService.DeleteProduct(ProductID);
                if (isDeleted) return Results.Ok(true);                
                else return Results.Problem("Error in deleting Product");                
            });

            return app;

        }
    }
}
