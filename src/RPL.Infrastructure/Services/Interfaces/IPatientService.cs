using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Result<Patient>> CreatePatientAsync(ApplicationUser model);

        Task<Result<long>> CreatePatientAsync(PatientDto patientDto);

        Task<IResult> DeletePatientAsync(long id);

        Task<Result<PatientDto>> GetPatientAsync(long id);

        Task<Result<Patient>> GetPatientByUserIdAsync(string userId);

        Task<Result<IEnumerable<PatientDto>>> GetPatientsAsync();

        Task<IResult> UpdatePatientAsync(long id, PatientDto patientDto);

    }
}
