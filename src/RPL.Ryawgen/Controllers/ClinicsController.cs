using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using RPL.Core.Filters;
using RPL.Core.Result;
using RPL.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        ///         %20latitude=16.8240209
        ///         %20longitude=96.1543727
        ///         %20radius=1000
        ///
        /// </remarks>
        /// <returns>A JSON array containing the jobs assigned to the driver.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<ClinicNearbyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ClinicNearbyFilter filter)
        {
            return Ok(await _clinicSearchService.GetNearbyClinicsAsync(filter));
        }
    }
}
