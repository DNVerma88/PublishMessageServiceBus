using PublishMessageServiceBus;

const string connectionString = "Service bus name space connection";
const string topicName = "mytopic";
PublishMessage publishMessage = new PublishMessage();
int num = 10;
for (int i = 0; i<num; i++)
{
  await publishMessage.PublishMessageToTopic(connectionString, topicName, i);
  Thread.Sleep(2000);
}
// Publish messages with ServiceBus Message Batch
await PublishMessageBatch.SentMessageBatch(connectionString, topicName);

Console.WriteLine("Press any key to end the application");
Console.ReadKey();
