using Azure.Messaging.ServiceBus;

namespace PublishMessageServiceBus
{
    public class PublishMessage
    {
        static ServiceBusSender sender;

        public async Task PublishMessageToTopic(string connectionString, string topicName, int number)
        {
            await using var client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(topicName);

            ServiceBusMessage message = new ServiceBusMessage($"Consumer 1 message. {number}");
            message.PartitionKey = "Subscriber1";
            message.MessageId = Guid.NewGuid().ToString();
            message.Subject = "consumer1"; // Added filter through portal on subject column for subscription1, filters can be added from C# code as well.
            // send the message in topic for subscription1 
            await sender.SendMessageAsync(message);

            Console.WriteLine($"Published Notification for Client 1. {number}");

            ServiceBusMessage message2 = new ServiceBusMessage($"Consumer 2 message. {number}");
            message2.PartitionKey = "Subscriber2";
            message2.MessageId = Guid.NewGuid().ToString();
            message2.Subject = "consumer2"; // Added filter through portal on subject column for subscription2, filters can be added from C# code as well.
            // send the message in topic for subscription2 
            await sender.SendMessageAsync(message2);

            Console.WriteLine($"Published Notification for Client 2. {number}");

        }

    }
}
