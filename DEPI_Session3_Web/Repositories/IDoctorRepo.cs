using DEPI_WebApi_Session2.Dtos.Doctor;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Repositories
{
    public interface IDoctorRepo
    {
        Task<PaginatedList<GetDoctorResponse>> GetallDoctorAsync(int pageNumber, int PageSize, string? searchTerm);
        Task<bool> DeleteDoctorAsync(int id);
        Task<bool> UpdateDoctorAsync(UpdateDoctorRequest request);
        Task<bool> CreateDoctorAsync(CreateDoctorRequest request);
    }
}
