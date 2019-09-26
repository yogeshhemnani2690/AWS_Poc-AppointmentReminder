using DynamoDb_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoaderLambda.Interfaces
{
   public interface ILoaderLambda
    {
        Task<Appointments> GetNextDayAppointmentIds(string nextDate);
    }
}
