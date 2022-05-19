using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishMessageServiceBus
{
    public static class PublishMessageBatch
    {
        static ServiceBusClient client;
        static ServiceBusSender sender;
        private const int numOfMessages = 3;

        public static async Task SentMessageBatch(string connectionString, string topicName)
        {
            client = new ServiceBusClient(connectionString);
            sender = client.CreateSender(topicName);
            using ServiceBusMessageBatch messageBatch1 = await sender.CreateMessageBatchAsync();
            using ServiceBusMessageBatch messageBatch2 = await sender.CreateMessageBatchAsync();
            for (int i = 1; i <= numOfMessages; i++)
            {
                var consumer1 = new ServiceBusMessage($"Consumer1 Message {i}");
                consumer1.PartitionKey = "Subscriber1";
                consumer1.MessageId = Guid.NewGuid().ToString();
                consumer1.Subject = "consumer1";
                if (!messageBatch1.TryAddMessage(consumer1))
                {
                    throw new Exception($"Something went wrong with consumer1 message {1}.");
                }
                var consumer2 = new ServiceBusMessage($"Consumer2 Message {i}");
                consumer2.PartitionKey = "Subscriber1";
                consumer2.MessageId = Guid.NewGuid().ToString();
                consumer2.Subject = "consumer1";
                if (!messageBatch2.TryAddMessage(consumer2))
                {
                    throw new Exception($"Something went wrong with consumer2 message {1}.");
                }
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch1);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the topic for consumer1.");
                await sender.SendMessagesAsync(messageBatch2);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the topic for consumer2.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
