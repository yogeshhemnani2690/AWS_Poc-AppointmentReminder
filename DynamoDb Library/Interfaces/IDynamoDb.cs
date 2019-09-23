using DynamoDb_Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDb_Library.Interfaces
{
    public interface IDynamoDb
    {
       void CreateDynamoDbTable();
        Task<Appointments> GetAppointments(int? id);
        Task<Appointments> GetNextDayAppointmentsAsync(string nextDate);
    }
}
 