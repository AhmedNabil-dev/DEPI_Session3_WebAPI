using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Appointment;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session3_Web.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public AppointmentRepo(ApplicationDBContext context, IMapper mapper)

        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<PaginatedList<GetAppointmentresponse>> GetAllAppointmentsAsync(int pageNumber, int PageSize, string? searchTerm)
        {

            List<Appointment> appointments;
            var totalRecords = 0;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                appointments = await _context.Appointments
                    .Where(a => a.PatientId.ToString().Contains(searchTerm) || a.DoctorId.ToString().Contains(searchTerm))
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Appointments
                    .CountAsync(a => a.PatientId.ToString().Contains(searchTerm) || a.DoctorId.ToString().Contains(searchTerm));
            }
            else
            {
                appointments = await _context.Appointments
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Appointments.CountAsync();
            }
            var response = new PaginatedList<GetAppointmentresponse>
            {
                pageNumber = pageNumber,
                PageSize = PageSize,
                TotalRecords = totalRecords,
                TotalPage = (int)Math.Ceiling((double)totalRecords / PageSize),
                Data = _mapper.Map<List<GetAppointmentresponse>>(appointments)
            };

            return response;
        }
        public async Task<bool> CreateAppointmentAsync(CreateAppiontmentRequest request)
        {
            var appointment = _mapper.Map<Appointment>(request);
            _context.Appointments.Add(appointment);
            var result =await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> UpdateAppointmentAsync(UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);
            appointment = _mapper.Map(request, appointment);
            _context.Update(appointment);
            var result =await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
          
            _context.Appointments.Remove(appointment);
           
            var result =await _context.SaveChangesAsync();
            return result > 0;
        }
     }
}
