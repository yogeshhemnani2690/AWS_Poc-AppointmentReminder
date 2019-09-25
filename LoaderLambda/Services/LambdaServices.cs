using Amazon.DynamoDBv2.Model;
using AppointmentIdQueue;
using DynamoDb_Library.DynamoDb;
using DynamoDb_Library.Models;
using LoaderLambda.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentReminder.Services
{
   
    public class LamdaServices : ILambdaService
    {
        private readonly SQSAppointmentIdQueue _sqsService;
        private readonly DynamoDbServices _dynamoDbServices;

        public LamdaServices(DynamoDbServices dynamoDbServices, SQSAppointmentIdQueue sqsService)
        {
            _dynamoDbServices = dynamoDbServices;
            _sqsService = sqsService;
        }

        public Task<Appointments> GetNextDayAppointmentIds(string nextDate)
        {
           Task<Appointments> response = _dynamoDbServices.GetNextDayAppointmentsAsync(nextDate);
           this.PostToQueue(response);
           return response;
        }
        
        public void PostToQueue(Task<Appointments> response)
        {
           List<Items> appointments = response.Result.Items.ToList();
            for (int i = 0; i < appointments.Count; i++)
            {
                int appointmentId = appointments[i].PocAppointmentId;
                _sqsService.SendAppointmentId(appointmentId);
            }
        }
      

    }
}
