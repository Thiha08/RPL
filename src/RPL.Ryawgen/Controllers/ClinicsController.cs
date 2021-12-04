using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class ClinicsController : BaseController
    {
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
        ///
        /// </remarks>
        /// <returns>A JSON array containing the jobs assigned to the driver.</returns>
        [HttpGet("NearbySearch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ClinicNearbyDto>>> SearchNearbyClinicsAsync(ClinicNearbyRequestDto request)
        {
            return Ok();
        }
    }
}
