using Ardalis.Result;
using RPL.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Core.Interfaces
{
    public interface IClinicService
    {
        Task<Result<IEnumerable<ClinicNearbyDto>>> GetAllNearbyClinicsAsync(ClinicNearbyRequestDto model);
    }
}
