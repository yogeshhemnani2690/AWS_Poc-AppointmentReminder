//using Amazon.DynamoDBv2.Model;
//using DynamoDb_Library.DynamoDb;
//using DynamoDb_Library.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AppointmentReminder.Services
//{
//    public class LamdaServices
//    {
//        private readonly DynamoDbServices _dynamoDbServices;

//        public LamdaServices(DynamoDbServices dynamoDbServices)
//        {
//            _dynamoDbServices = dynamoDbServices;
//        }

//        public Task<Appointments> GetNextDayAppointmentIds(string nextDate)
//        {
//           Task<Appointments> response = _dynamoDbServices.GetNextDayAppointmentsAsync(nextDate);
//           return response;
//        }
        
//        public void send()
//        {

//        }
      

//    }
//}
