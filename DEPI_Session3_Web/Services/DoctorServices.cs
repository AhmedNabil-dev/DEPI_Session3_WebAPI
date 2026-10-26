using Azure.Core;
using DEPI_Session3_Web.Repositories;
using DEPI_WebApi_Session2.Dtos.Doctor;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Services
{
    public class DoctorServices(IDoctorRepo doctorRepo) : IDoctorServices
    {
        public Task<bool> CreateDoctorAsync(CreateDoctorRequest request)
        {
            return doctorRepo.CreateDoctorAsync(request);
        }

        public Task<bool> DeleteDoctorAsync(int id)
        {
            return doctorRepo.DeleteDoctorAsync(id);
        }

        public Task<PaginatedList<GetDoctorResponse>> GetallDoctorAsync(int pageNumber, int PageSize, string? searchTerm)
        {
            return doctorRepo.GetallDoctorAsync(pageNumber,PageSize,searchTerm);
        }

        public Task<bool> UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            return doctorRepo.UpdateDoctorAsync(request);
        }
    }
}
