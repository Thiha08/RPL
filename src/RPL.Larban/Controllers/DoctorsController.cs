using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs.Doctors;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Infrastructure.Data.Initializer;
using RPL.Infrastructure.Services.Interfaces;
using System;
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
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService, IUserService userService)
        {
            _doctorService = doctorService;
            _userService = userService;
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
        /// Generate dummy doctors
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/doctors/generateDummy
        ///
        /// </remarks>
        [HttpPost("generateDummy")]
        [ProducesResponseType(typeof(Result<IEnumerable<DoctorDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateDummyDoctorsAsync()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                return BadRequest();

            var createdDoctors = new List<Doctor>();
            var dummyDoctors = IdentityDbInitializer.GetDummyDoctorUsers();

            foreach (var newUser in dummyDoctors)
            {
                var userResult = await _userService.CreateUserAsync(newUser, IdentityConstants.DefaultPassword, Roles.Patient);

                if (userResult.IsSuccess)
                {
                    var doctorResult = await _doctorService.CreateDoctorAsync(newUser);
                    createdDoctors.Add(doctorResult);
                }
            }

            return Ok(createdDoctors);
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
