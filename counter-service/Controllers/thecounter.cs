using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace counter_service.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    
    public class thecounter : ControllerBase
    {
       
        private string connectionString = "amqps://ugqphoka:fceSNdG1qqY5g65S_-RwV2ISfpaKA92H@stingray.rmq.cloudamqp.com/ugqphoka";
        private string queueName = "counter";

        private uint GetMessageCount()
        {
            Uri uri = new Uri(connectionString);
            var factory = new ConnectionFactory() { Uri = uri };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                return channel.MessageCount(queueName);
            }
        }
 
        private async void Send(string msg)
        {
            await Task.Run(() =>
            {
                Uri uri = new Uri(connectionString);
                var factory = new ConnectionFactory() { Uri = uri };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    string message = msg;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                }
            });
        }
        /// <summary>
        /// output the queue count 
        /// </summary>
        [HttpGet]
        public uint Get()
        {
           return GetMessageCount();
        }
        /// <summary>
        /// send the datetime to the queue in async function and return string that indicate that the Post
        /// request registered successfully.
        /// </summary>
        [HttpPost]
        public string Post () 
        {
            DateTime dateTime = DateTime.Now;
            Send(dateTime.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
            return "a post request registered successfully ";
        }
    }
}
