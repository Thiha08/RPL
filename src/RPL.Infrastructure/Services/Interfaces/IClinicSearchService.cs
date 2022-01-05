using RPL.Core.DTOs;
using RPL.Core.Filters;
using RPL.Core.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IClinicSearchService
    {
        Task<Result<IEnumerable<ClinicNearbyDto>>> GetNearbyClinicsAsync(ClinicNearbyFilter filter);
    }
}
