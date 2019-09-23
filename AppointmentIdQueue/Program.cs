using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;


namespace AppointmentIdQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a new SQS...");
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
            var sqsRequest = new CreateQueueRequest()
            {
                QueueName = "MailerQueue"
            };
            var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;
            var mailerQueueUrl = createQueueResponse.QueueUrl;



            var listQueueRequest = new ListQueuesRequest();
            var listQueueResponse = sqs.ListQueuesAsync(listQueueRequest);

            foreach (var queues in listQueueResponse.Result.QueueUrls)
            {
                Console.WriteLine($"Available Queues : {queues}");
            }


            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = mailerQueueUrl,
                MessageBody = "MailId: xyz@xyz.com"
            };
            sqs.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("Message Sent in Queue");

            //Receiving Message
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = mailerQueueUrl
            };

            var receiveMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest).Result;
            foreach (var message in receiveMessageResponse.Messages)
            {
                Console.WriteLine("Messages");
                Console.WriteLine($"MessageId :{message.MessageId}");
                Console.WriteLine($"ReceiptHandle :{message.ReceiptHandle}");
                Console.WriteLine($"MSD5Body :{message.MD5OfBody}");
                Console.WriteLine($"Body :{message.Body}");

                foreach (var attributes in message.Attributes)
                {
                    Console.WriteLine($"Name  :{attributes.Key}");
                    Console.WriteLine($"Value :{attributes.Value}");
                }

                var messageReceiptHandle = message.ReceiptHandle;
                var deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = mailerQueueUrl,
                    ReceiptHandle = messageReceiptHandle
                };
                sqs.DeleteMessageAsync(deleteRequest);
            }
            Console.ReadLine();
        }
    }
}
