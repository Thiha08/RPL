using RPL.Core.DTOs.Doctors;
using RPL.Core.Entities;
using RPL.Core.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Result> AssignToClinicAsync(long id, long clinicId);

        Task<Result<Doctor>> CreateDoctorAsync(ApplicationUser model);

        Task<Result<long>> CreateDoctorAsync(DoctorDto doctorDto);

        Task<IResult> DeleteDoctorAsync(long id);

        Task<Result<DoctorDto>> GetDoctorAsync(long id);

        Task<Result<IEnumerable<DoctorDto>>> GetDoctorsAsync();

        Task<Result<IEnumerable<AvailableDoctorDto>>> GetAvailableDoctorsAsync();

        Task<Result> UnassignFromClinicAsync(long id, long clinicId);

        Task<IResult> UpdateDoctorAsync(long id, DoctorDto doctorDto);
    }
}
