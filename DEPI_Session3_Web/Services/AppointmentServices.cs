using DEPI_Session3_Web.Repositories;
using DEPI_WebApi_Session2.Dtos.Appointment;
using DEPI_WebApi_Session2.Results;

namespace DEPI_Session3_Web.Services
{
    public class AppointmentServices(IAppointmentRepo appointmentRepo) : IAppointmentServices
    {
        public Task<bool> CreateAppointmentAsync(CreateAppiontmentRequest request)
        {
            return appointmentRepo.CreateAppointmentAsync(request);
        }

        public Task<bool> DeleteAppointmentAsync(int id)
        {
            return appointmentRepo.DeleteAppointmentAsync(id);
            throw new NotImplementedException();
        }

        public Task<PaginatedList<GetAppointmentresponse>> GetAllAppointmentsAsync(int pageNumber, int PageSize, string? searchTerm)
        {
            return appointmentRepo.GetAllAppointmentsAsync(pageNumber,PageSize,searchTerm);
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAppointmentAsync(UpdateAppointmentRequest request)
        {
            return appointmentRepo.UpdateAppointmentAsync(request);
          
        }
    }
}
