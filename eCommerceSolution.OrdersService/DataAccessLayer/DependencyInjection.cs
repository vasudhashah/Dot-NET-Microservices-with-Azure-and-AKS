﻿using DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionStringTemplate = configuration.GetConnectionString("MongoDB")!;
            string connectionString = connectionStringTemplate
              .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST"))
              .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT"));

            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

            services.AddScoped<IMongoDatabase>(provider =>
            {
                IMongoClient client = provider.GetRequiredService<IMongoClient>();
                return client.GetDatabase("OrdersDatabase");
            });
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
