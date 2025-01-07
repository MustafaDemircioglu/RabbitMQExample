using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using RabbitMQ.Client;
using RabbitMQExample.UIConsole.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitMQExample.UIConsole.PersistenceLayer
{
    public class SqlOrderRepository : IOrderRepository
    {

        private readonly string _connectionString = "server=.;database=Northwind;integrated security=true;TrustServerCertificate=True";

        public  void AddOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                var query = "INSERT INTO Orders ( CustomerID, OrderDate, RequiredDate, Freight) VALUES (@CustomerID, @OrderDate, @RequiredDate, @Freight)";

                // OrderID'nin otomatik olarak atanmasını sağlıyoruz
                connection.Execute(query, new
                {
                    order.CustomerId,
                    order.OrderDate,
                    order.RequiredDate,
                    order.Freight
                });
;            }
        }


    } 
}

