using DynamoDb_Library.DynamoDb;
using DynamoDb_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentReminder.Services
{
    public class LamdaServices
    {
        readonly DynamoDbServices _dynamoDbServices;

     

        public async Task<Appointments> GetNextDayAppointmentIds(string nextDate)
        {
            Appointments nextDayAppointments = await _dynamoDbServices.GetNextDayAppointmentsAsync(nextDate);
            return nextDayAppointments;
        }
        public void send()
        {

        }
        void receive()
        {

        }

    }
}
