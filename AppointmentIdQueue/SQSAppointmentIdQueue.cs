using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentIdQueue
{
    class SQSAppointmentIdQueue
    {
        IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
        string AppointmentIdQueueUrl;
        Appointments nextDayAppointments = new Appointments();



        public void SendAppointmentId()
        {
            this.AppointmentIdQueueUrl = sqs.GetQueueUrlAsync("AppointmentIdQueue").Result.QueueUrl;
            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = AppointmentIdQueueUrl,
                MessageBody = Convert.ToString(nextDayAppointments.PocAppointmentId)
            };
            sqs.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("AppointmentId Sent in AppointmentIdQueue");
        }


        public void ReceiveAppointmentId()
        {
            this.AppointmentIdQueueUrl = sqs.GetQueueUrlAsync("AppointmentIdQueue").Result.QueueUrl;
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = AppointmentIdQueueUrl
            };
            var receiveMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest).Result;
            List<Appointments> appointments = new List<Appointments>();
            foreach (var AppointmentId in receiveMessageResponse.Messages)
            {
                Console.WriteLine($"ReceiptHandle :{AppointmentId.ReceiptHandle}");
                Console.WriteLine($"MSD5Body :{AppointmentId.MD5OfBody}");
                Console.WriteLine($"Body :{AppointmentId.Body}");
                Appointments appointment = new Appointments();
                appointment.PocAppointmentId = Convert.ToInt32(AppointmentId.Body);
            }
           Console.WriteLine("AppointmentIds Fetched from AppointmentIdQueue");
        }

    }
}
