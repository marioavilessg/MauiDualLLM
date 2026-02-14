using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Services
{
    public class RabbitMqService
    {
        private IConnection _connection;
        private IModel _channel;

        private string _queueIn;
        private string _queueOut;

        public event Action<string>? MessageReceived;

        public void Connect(string host, string username, string password,
                    string queueIn, string queueOut)
        {
            var factory = new ConnectionFactory()
            {
                HostName = host,
                UserName = username,
                Password = password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _queueIn = queueIn;
            _queueOut = queueOut;

            _channel.QueueDeclare(_queueIn, false, false, false);
            _channel.QueueDeclare(_queueOut, false, false, false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                MessageReceived?.Invoke(message);
            };

            _channel.BasicConsume(_queueIn, true, consumer);
        }


        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "",
                routingKey: _queueOut,
                basicProperties: null,
                body: body);
        }
    }
}
