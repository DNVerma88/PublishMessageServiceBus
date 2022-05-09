// See https://aka.ms/new-console-template for more information
using PublishMessageServiceBus;

PublishMessage publishMessage = new PublishMessage();
await publishMessage.PublishMessageToTopic();