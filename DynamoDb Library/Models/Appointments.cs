using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDb_Library.Models
{
    public class Appointments
    {
        public IEnumerable<Items> Items { get; set; }
       
    }

    public class Items
    {
        public int PocAppointmentId { get; set; }
        public string PocAppointmentDate { get; set; }
        public string PocEmail { get; set; }
        public string PocPatientName { get; set; }
        public string PocDoctorName { get; set; }
    }
}
