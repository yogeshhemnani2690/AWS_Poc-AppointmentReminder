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
        string AppointmentIdQueueUrl;// need to set this  from projects appsetting.json file
        Appointments nextDayAppointments = new Appointments();
        public void SendMessageRequest()
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = AppointmentIdQueueUrl,
                MessageBody = Convert.ToString(nextDayAppointments.PocAppointmentId)
            };
            sqs.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("AppointmentId Sent in AppointmentIdQueue");
        }
    }
}
