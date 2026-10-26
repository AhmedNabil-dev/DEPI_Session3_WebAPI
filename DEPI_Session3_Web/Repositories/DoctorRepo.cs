using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Doctor;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session3_Web.Repositories
{
    public class DoctorRepo : IDoctorRepo
    {

        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DoctorRepo(ApplicationDBContext context) 
        {
            _context = context;
        }
         
        public async Task<PaginatedList<GetDoctorResponse>> GetallDoctorAsync(int pageNumber, int PageSize, string? searchTerm)
        {
            List<Doctor> Doctors;
            var totalCount = 0;
            if (searchTerm is not null)
            {
                Doctors = await _context.Doctor
                                  .Where(d => d.Name.Contains(searchTerm))
                                  .Skip((pageNumber - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();
                totalCount = await _context.Doctor.CountAsync(d => d.Name.Contains(searchTerm));
            }
            else
            {
                Doctors = await _context.Doctor
                                  .Skip((pageNumber - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();
                totalCount = await _context.Doctor.CountAsync();
            }
            var Doctorresponse = _mapper.Map<List<Doctor>, List<GetDoctorResponse>>(Doctors);
            var paginatedList = new PaginatedList<GetDoctorResponse>()
            {
                PageSize = PageSize,
                pageNumber = pageNumber,
                TotalRecords = totalCount,
                TotalPage = (int)Math.Ceiling(totalCount / ((double)pageNumber * PageSize)),
                Data = Doctorresponse
            };

            return paginatedList;
        }

        public async Task<bool> CreateDoctorAsync(CreateDoctorRequest request)
        {
            var doctor = _mapper.Map<Doctor>(request);


            await _context.Doctor.AddAsync(doctor);
           var result = await _context.SaveChangesAsync();
            return result >0;
        }
        public async Task<bool> UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            var doctor = await _context.Doctor.FindAsync(request.Id);


            doctor = _mapper.Map(request, doctor);


            _context.Doctor.Update(doctor);
            var result = await _context.SaveChangesAsync();
            return result >0;
        }
        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var Doctor = await _context.Doctor.FindAsync(id);


            _context.Doctor.Remove(Doctor);
            var result = await _context.SaveChangesAsync();
            return result >0;
        }

    }
}
