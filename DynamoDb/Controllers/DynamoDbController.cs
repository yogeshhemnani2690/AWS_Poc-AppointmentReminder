using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DynamoDb_Library.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace DynamoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/DynamoDb")]
    [ApiController]
    [EnableCors("CORSPolicy")]

    public class DynamoDbController : Controller
    {
       private readonly IDynamoDb _dynamoDb;

        public DynamoDbController(IDynamoDb dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        [Route("CreateTable")]
        public IActionResult CreateTable()
        {
            _dynamoDb.CreateDynamoDbTable();
            return Ok();
        }


        [Route("GetAppointments")]
        public async Task<IActionResult> GetAppointments([FromQuery]int? pocAppointmentId)
        {
            var response = await _dynamoDb.GetAppointments(pocAppointmentId);
            return Ok(response);
        }


        [Route("GetNextDayAppointmentIds")]
        public async Task<IActionResult> GetAppoitnmentIds()
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDateTime = currentDate.AddDays(1);
            string nextDate = nextDateTime.ToString("dd/MM/yyyy");
            var response = await _dynamoDb.GetNextDayAppointmentsAsync(nextDate);
            return Ok(response);
        }
    }
}