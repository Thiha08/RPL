using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class ClinicsController : BaseController
    {
        private readonly IClinicSearchService _clinicSearchService;

        public ClinicsController(
            IClinicSearchService clinicSearchService,
            IPatientService patientService)
            : base(patientService)
        {
            _clinicSearchService = clinicSearchService;
        }

        /// <summary>
        /// Get a list of clinic nearby with scheduled doctors.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/clinics/nearbySearch
        ///         ?keyword=paraman
        ///         &#38;latitude=16.8240209
        ///         &#38;longitude=96.1543727
        ///         &#38;radius=1000
        ///
        /// </remarks>
        [HttpGet("nearbySearch")]
        [ProducesResponseType(typeof(Result<IEnumerable<ClinicNearbyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ClinicNearbyFilter filter)
        {
            return Ok(await _clinicSearchService.GetNearbyClinicsAsync(filter));
        }
    }
}
