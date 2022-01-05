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

        Task<Result<IEnumerable<AvailableDoctorDto>>> GetAvailableDoctorsAsync();

        Task<Result> UnassignFromClinicAsync(long id, long clinicId);
    }
}
