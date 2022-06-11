
using Confluent.Kafka;
using EAuction.Service.MongoDb.Buyer;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAuction.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                GroupId = "test_group",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("test");
                while (true)
                {
                    var cr = consumer.Consume();
                    //var buerInfo = JsonSerializer.Deserialize
                    //        <BuyerInfo>
                    //            (cr.Message.Value);
                    Console.WriteLine(cr.Message.Value);

                    using (HttpClient client = new HttpClient())
                    {
                        //client.BaseAddress = new Uri("http://localhost:54741/");
                        client.DefaultRequestHeaders.Accept.Clear();
                      //  client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var data = new StringContent(cr.Message.Value, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44360/buyer", data);                                               
                    }
                }
            }
        }
    }
}
