using Azure.Core;
using DEPI_Session3_Web.Repositories;
using DEPI_WebApi_Session2.Dtos.Patient;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Services
{
    public class PatientServices(IPatientRepo patientRepo) : IPatientServices
    {
        public Task<bool> CreatePatientAsync(CreatePatientRequest request)
        {
            return patientRepo.CreatePatientAsync(request);
        }

        public Task<bool> DeletePatientAsync(int id)
        {
            return patientRepo.DeletePatientAsync(id);
        }

        public Task<PaginatedList<GetPatientResponse>> GetAllPatientsAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            return patientRepo.GetAllPatientsAsync(pageNumber,pageSize,searchTerm);
        }

        public Task<bool> updatePatientAsync(UpdatePatientRequest request)
        {
            return patientRepo.updatePatientAsync(request);
        }
    }
}
