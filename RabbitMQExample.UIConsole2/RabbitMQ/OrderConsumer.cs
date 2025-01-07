using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.UIConsole2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.UIConsole2.RabbitMQ
{
    public class OrderConsumer
    {
        private readonly string _connectionString = "server=.;database=Northwind;integrated security=true;TrustServerCertificate=True";

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "orders_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Mesajı deseralize et
                    var order = JsonConvert.DeserializeObject<Order>(message);

                    // Veritabanına kaydedelim
                    int orderId = SaveOrder(order);  // Siparişi kaydet ve OrderID'yi al
                    SaveOrderDetails(orderId, order);  // Sipariş detaylarını kaydet

                    // Mesajı başarılı şekilde işlediğimizi onaylayalım
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                    Console.WriteLine($"[x] Sipariş işlendi ve veritabanına kaydedildi: {message}");
                };

                // Kuyruğu dinlemeye başla
                channel.BasicConsume(queue: "orders_queue",
                                     autoAck: false,  // Mesajları otomatik onaylama
                                     consumer: consumer);

                Console.WriteLine("Tüketici çalışıyor... Çıkmak için [CTRL+C] tuşuna basın.");
                Console.ReadLine();
            }
        }

        // Siparişi veritabanına kaydetme (Order tablosu)
        private int SaveOrder(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Siparişi Orders tablosuna ekle
                var sql = @"
                INSERT INTO Orders (CustomerID, OrderDate, ShippedDate, ShipAddress, ShipCity)
                VALUES (@CustomerID, @OrderDate, @ShippedDate, @ShipAddress, @ShipCity);
                SELECT CAST(SCOPE_IDENTITY() as INT);";  // OrderID'yi döndürmek için

                return connection.Query<int>(sql, order).Single();  // OrderID'yi al
            }
        }

        // Sipariş detaylarını veritabanına kaydetme (OrderDetails tablosu)
        private void SaveOrderDetails(int orderId, Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Siparişin detaylarını ekle
                foreach (var detail in order.OrderDetails)  // OrderDetails listeyi siparişle birlikte gönderdiğimiz varsayalım
                {
                    var sql = @"
                    INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
                    VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice);";

                    // Sipariş detaylarını ekle
                    connection.Execute(sql, new { OrderID = orderId, detail.ProductID, detail.Quantity, detail.UnitPrice });
                }
            }
        }
    }
}
