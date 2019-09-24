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
    [Produces("application/json")]

    [Route("api/Loader")]
    [ApiController]
    public class LambdaController : ControllerBase
    {
        readonly LamdaServices _lambdaServices;

        public LambdaController(LamdaServices lambdaServices)
        {
            _lambdaServices = lambdaServices;
        }
        
        [Route("GetNextDayAppointmentIds")]
        public async Task<IActionResult> GetAppoitnmentIds()
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDateTime = currentDate.AddDays(1);
            string nextDate = nextDateTime.ToString("dd/MM/yyyy");
            var nextDayAppointments = await _lambdaServices.GetNextDayAppointmentIds(nextDate);
            return Ok(nextDayAppointments);
        }


    }
}