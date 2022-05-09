using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishMessageServiceBus
{
    public class PublishMessage
    {
        static string connectionString = "*****";

        static string topicName = "notification0";

        static ServiceBusSender sender;


        public async Task PublishMessageToTopic()
        {
            await using var client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(topicName);

            ServiceBusMessage message = new ServiceBusMessage("Pushing message for consumer 1.");
            message.PartitionKey = "Subscriber1";
            message.MessageId = Guid.NewGuid().ToString();
            message.Subject = "consumer1"; // Added filter through portal on subject column for subscription1, filters can be added from C# code as well.
            // send the message in topic for subscription1 
            await sender.SendMessageAsync(message);

            ServiceBusMessage message2 = new ServiceBusMessage("Pushing message for consumer 2.");
            message2.PartitionKey = "Subscriber2";
            message2.MessageId = Guid.NewGuid().ToString();
            message2.Subject = "consumer2"; // Added filter through portal on subject column for subscription2, filters can be added from C# code as well.
            // send the message in topic for subscription2 
            await sender.SendMessageAsync(message2);



            Console.WriteLine("Press any key to end the application");
            Console.ReadKey();
        }

    }
}
