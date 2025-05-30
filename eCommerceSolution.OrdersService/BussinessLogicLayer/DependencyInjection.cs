﻿using BussinessLogicLayer.Mappers;
using BussinessLogicLayer.ServiceContracts;
using BussinessLogicLayer.Services;
using BussinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //TO DO: Add business logic layer services into the IoC container
            services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();

            services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile).Assembly);

            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }
    }
}
