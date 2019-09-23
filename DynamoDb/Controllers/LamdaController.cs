using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentReminder.Services;
using DynamoDb_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentReminder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LamdaController : ControllerBase
    {
        readonly LamdaServices _lamdaServices;

        public LamdaController(LamdaServices lamdaServices)
        {
            _lamdaServices = lamdaServices;
        }

        //[HttpPost("{PocAppointmentId}")]
        //public IActionResult PostToQueue(int PocAppointmentId)
        //{
        //calls getNexxtdayAppointments via lamda service
        //}

        [Route("GetNextDayAppointmentIds")]
        public async Task<IActionResult> GetAppoitnmentIds()
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDateTime = currentDate.AddDays(1);
            string nextDate = nextDateTime.ToString("dd/MM/yyyy");
            Appointments nextDayAppointments = await _lamdaServices.GetNextDayAppointmentIds(nextDate);
            return Ok(response);
        }
    }
}