using DEPI_WebApi_Session2.Dtos.Patient;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Services
{
    public interface IPatientServices
    {
        Task<PaginatedList<GetPatientResponse>> GetAllPatientsAsync(int pageNumber, int pageSize, string? searchTerm);
        Task<bool> DeletePatientAsync(int id);
        Task<bool> updatePatientAsync(UpdatePatientRequest request);
        Task<bool> CreatePatientAsync(CreatePatientRequest request);
    }
}
