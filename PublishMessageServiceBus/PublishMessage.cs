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
        static string connectionString = "Endpoint=sb://notificationservice0.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=b4ojYUDXIBKvtL6s1IN4JsNHUvG/KGVzIv33ta0nBHg=";

        static string topicName = "notification0";

        static ServiceBusSender sender;


        public async Task PublishMessageToTopic()
        {
            await using var client = new ServiceBusClient(connectionString);
            // create the sender
            sender = client.CreateSender(topicName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage("Hello world!");
            message.PartitionKey = "Testing_Publisher";
            message.MessageId = Guid.NewGuid().ToString();
            // send the message
            await sender.SendMessageAsync(message);



            Console.WriteLine("Press any key to end the application");
            Console.ReadKey();
        }

    }
}
