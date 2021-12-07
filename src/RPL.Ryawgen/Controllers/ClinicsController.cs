using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using RPL.Core.Filters;
using RPL.Core.Result;
using RPL.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class ClinicsController : BaseController
    {
        private readonly IClinicSearchService _clinicSearchService;

        public ClinicsController(IClinicSearchService clinicSearchService)
        {
            _clinicSearchService = clinicSearchService;
        }

        /// <summary>
        /// Get a list of clinic nearby with scheduled doctors.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Clinics/NearbySearch
        ///         ?keyword=paraman
        ///         &latitude=16.8240209
        ///         &longitude=96.1543727
        ///         &radius=1000
        ///         &page=1
        ///         &pageSize=10
        ///
        /// </remarks>
        /// <returns>A JSON array containing the jobs assigned to the driver.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<ClinicNearbyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ClinicNearbyFilter filter)
        {
            filter = filter ?? new ClinicNearbyFilter();

            filter.LoadChildren = true;
            filter.IsPagingEnabled = true;

            return Ok(await _clinicSearchService.GetNearbyClinicsAsync(filter));
        }
    }
}
