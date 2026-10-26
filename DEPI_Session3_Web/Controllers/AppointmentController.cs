using AutoMapper;
using Azure.Core;
using DEPI_EF_Session14.Domain;
using DEPI_Session3_Web.Repositories;
using DEPI_Session3_Web.Services;
using DEPI_WebApi_Session2.Dtos.Appointment;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentServices _appointmentservice;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IAppointmentServices appointmentservice, ILogger<AppointmentController> logger)
        {
            _appointmentservice = appointmentservice;
            _logger = logger;
        }   
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAppointments(int pageNumber, int PageSize, string? searchTerm)
        {

            var response = await _appointmentservice.GetAllAppointmentsAsync(pageNumber, PageSize, searchTerm);
            var result = GenericResult<PaginatedList<GetAppointmentresponse>>.Success(response);
            return Ok(result);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment(CreateAppiontmentRequest request)
        {

            var result = await _appointmentservice.CreateAppointmentAsync(request);
            return result ? Created() : BadRequest();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAppointment( UpdateAppointmentRequest request)
        {
            var result = await _appointmentservice.UpdateAppointmentAsync(request);
            return result ? Ok() :BadRequest() ;
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentservice.DeleteAppointmentAsync(id);
            return result ? NoContent() : BadRequest();
            
        }

    }
}
