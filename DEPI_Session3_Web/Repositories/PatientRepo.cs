using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Patient;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DEPI_Session3_Web.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PatientRepo(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetPatientResponse>> GetAllPatientsAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            List<Patient> patients;
            var totalRecords = 0;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                patients = await _context.Patients
                    .Where(p => p.Name.Contains(searchTerm))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                totalRecords = await _context.Patients.CountAsync(p => p.Name.Contains(searchTerm));
            }
            else
            {
                patients = await _context.Patients
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                totalRecords = await _context.Patients.CountAsync();
            }
            var patientResponse = _mapper.Map<List<GetPatientResponse>>(patients);
            var PaginationResponse = new PaginatedList<GetPatientResponse>()
            {
                pageNumber = pageNumber,
                PageSize =  pageSize,
                TotalRecords = totalRecords,
                TotalPage = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = patientResponse
            };
            return PaginationResponse;
        }

        public async Task<bool> CreatePatientAsync(CreatePatientRequest request)
        {
            var patient = _mapper.Map<Patient>(request);
            _context.Patients.Add(patient);
           var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> updatePatientAsync(UpdatePatientRequest request)
        {
            var patient = await _context.Patients.FindAsync(request.Id);
            if (patient == null)
            {
                return false;
            }
            _mapper.Map(request, patient);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }
            _context.Patients.Remove(patient);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
