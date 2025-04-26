﻿using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBussinessLogicLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);

            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidators>();
            
            services.AddScoped<IProductsService, ProductsService>();
            return services;
        }
    }
}
