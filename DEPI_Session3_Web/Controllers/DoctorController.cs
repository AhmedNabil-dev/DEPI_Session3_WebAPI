using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_Session3_Web.Repositories;
using DEPI_Session3_Web.Services;
using DEPI_WebApi_Session2.Dtos.Doctor;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices _doctorService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorServices doctorservice,  ILogger<DoctorController> logger)
        {
            _doctorService = doctorservice;
            _logger = logger;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetallDoctors(int pageNumber, int PageSize, string? searchTerm)
        {



            var Doctors = await _doctorService.GetallDoctorAsync(pageNumber, PageSize, searchTerm);

            if (Doctors == null)
            {
                _logger.LogWarning("No doctors found.");
                return NotFound("No doctors found.");
            }
            var result = GenericResult<PaginatedList<GetDoctorResponse>>.Success(Doctors);
            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateDoctorRequest request)
        {

            var result = await _doctorService.CreateDoctorAsync(request);
            return result ? Created() : BadRequest(); 
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> update(UpdateDoctorRequest request)
        {


            var result = await _doctorService.UpdateDoctorAsync(request);

            return result ? Ok("Doctor updated successfully.") : BadRequest() ;
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            var result = await _doctorService.DeleteDoctorAsync(id);

            return result ? Ok("Doctor deleted successfully.") : BadRequest();
        }


    }
}
