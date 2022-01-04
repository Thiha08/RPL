using RPL.Core.DTOs;
using RPL.Core.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Interfaces
{
    public interface IClinicService
    {
        Task<Result<long>> CreateClinicAsync(ClinicDto clinicDto);

        Task<IResult> DeleteClinicAsync(long id);

        Task<Result<ClinicDto>> GetClinicAsync(long id);

        Task<Result<IEnumerable<ClinicDto>>> GetClinicsAsync();

        Task<IResult> UpdateClinicAsync(long id, ClinicDto clinicDto);
    }
}
