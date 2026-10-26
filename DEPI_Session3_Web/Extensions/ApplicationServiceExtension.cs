using DEPI_Session3_Web.Repositories;
using DEPI_WebApi_Session2.Helper;
using DEPI_WebApi_Session2.Infrestraction.Data;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session3_Web.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServies(this IServiceCollection Services, IConfiguration configuration)
        {
            // IoC
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<patientProfile>();
                cfg.AddProfile<AppointmentProfile>();
            });

            Services.AddDbContext<ApplicationDBContext>(option =>
              {
                  option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
              });
            Services.AddScoped<IPatientRepo, PatientRepo>();
            Services.AddScoped<IDoctorRepo, DoctorRepo>();
            Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            Services.AddScoped(typeof(IGenericRepositroy<>), typeof(GenericRepository<>));

            return Services;
        }
    }
}
