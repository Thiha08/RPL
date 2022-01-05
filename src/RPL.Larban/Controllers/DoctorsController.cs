using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs.Doctors;
using RPL.Core.Result;
using RPL.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Larban.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [AllowAnonymous]
    public class DoctorsController : BaseController
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Get available doctors
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/doctors/available
        ///
        /// </remarks>
        [HttpGet("available")]
        [ProducesResponseType(typeof(Result<IEnumerable<AvailableDoctorDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableDoctorsAsync()
        {
            return Ok(await _doctorService.GetAvailableDoctorsAsync());
        }

        /// <summary>
        /// Assign doctor to clinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/doctors/5/assignToClinic/3
        ///
        /// </remarks>
        [HttpPut("{id}/assignToClinic/{clinicId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignToClinicAsync(long id, long clinicId)
        {
            return Ok(await _doctorService.AssignToClinicAsync(id, clinicId));
        }

        /// <summary>
        /// Unassign doctor from clinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/doctors/5/unassignFromClinic/3
        ///
        /// </remarks>
        [HttpPut("{id}/unassignFromClinic/{clinicId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> UnAssignFromClinicAsync(long id, long clinicId)
        {
            return Ok(await _doctorService.UnassignFromClinicAsync(id, clinicId));
        }
    }
}
