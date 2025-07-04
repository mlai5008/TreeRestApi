﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using WebAppCqrsMediator.Domain.MessageQueues;

namespace WebAppCqrsMediator.Infrastructure.MessageQueues
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("node", exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: "node", body: body);
        }
    }
}
