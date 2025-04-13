﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace eCommerce.Infrastructure.DbContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        //Creating DB Connection
        public DapperDbContext(IConfiguration configuration)
        {
         _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("PostgresConnection");

            //Create a new NpgsqlConnection with the retrived connection string
           _connection = new NpgsqlConnection(connectionString);
        }
        public IDbConnection DbConnection => _connection;
    }

    
}
