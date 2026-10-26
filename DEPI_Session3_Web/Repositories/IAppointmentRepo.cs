using DEPI_WebApi_Session2.Dtos.Appointment;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Repositories
{
    public interface IAppointmentRepo
    {
        Task<PaginatedList<GetAppointmentresponse>> GetAllAppointmentsAsync(int pageNumber, int PageSize, string? searchTerm);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<bool> UpdateAppointmentAsync(UpdateAppointmentRequest request);
        Task<bool> CreateAppointmentAsync(CreateAppiontmentRequest request);

    }
}
