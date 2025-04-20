﻿using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts
{
    public interface IProductsService
    {
        /// <summary>
        /// Retrieves the list of products from the prodcts repository
        /// </summary>
        /// <returns>Returns list of ProductResponse objects</returns>
        Task<List<ProductResponse?>> GetProducts();

        /// <summary>
        /// Retrieves list of products matching with given condition
        /// </summary>
        /// <param name="conditionExpression">Expression that represents condition to check</param>
        /// <returns>Returns matching products</returns>
        Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

        /// <summary>
        /// Adds (inserts) product into the table using products repository
        /// </summary>
        /// <param name="productAddRequest">Product to insert</param>
        /// <returns>Product after inserting or null if unsuccessful</returns>
        Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

        /// <summary>
        /// Updates the existing product based on the ProductID
        /// </summary>
        /// <param name="productUpdateRequest">Product data to update</param>
        /// <returns>Returns product object after successful updation; otherwise null</returns>
        Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);


        /// <summary>
        /// Deletes an existing product based on given product id
        /// </summary>
        /// <param name="productID">ProductID to search and delete</param>
        /// <returns>Returns true if the deletion is successful; otherwise false</returns>
        Task<bool> DeleteProduct(Guid productID);

    }
}
