using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_Session3_Web.Repositories;
using DEPI_Session3_Web.Services;
using DEPI_WebApi_Session2.Dtos.Patient;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientServices _patientservice;
        private readonly ILogger<PatientController> _logger;
        public PatientController(IPatientServices patientservice,  ILogger<PatientController> logger)
        {
            _patientservice = patientservice;
            _logger = logger;
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetallPatient(int pageNumber, int PageSize, string? searchTerm)
        {
            var patients = await _patientservice.GetAllPatientsAsync(pageNumber, PageSize, searchTerm);
            if (patients == null )
            {
                _logger.LogWarning("No patients found in the database.");
                return NotFound("No patients found.");
            }
            var result =  GenericResult<PaginatedList<GetPatientResponse>>.Success(patients);
            
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePatient(CreatePatientRequest request)
        {
          var result = await _patientservice.CreatePatientAsync(request);

            return result ? Created() : BadRequest();
        }

        [HttpPut("update/{id}")]
       public async Task<IActionResult>  UpdatePatient(UpdatePatientRequest request)
        {
            var result = await _patientservice.updatePatientAsync(request);

            return result ? Ok(result) : BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientservice.DeletePatientAsync(id);

            return result ? NoContent() : BadRequest();
        }


    }
}
