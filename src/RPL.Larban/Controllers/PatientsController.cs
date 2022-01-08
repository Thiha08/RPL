using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
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
    public class PatientsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;

        public PatientsController(IUserService userService, IPatientService patientService)
        {
            _userService = userService;
            _patientService = patientService;
        }

        /// <summary>
        /// Generate dummy patients
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/patients/generateDummy
        ///
        /// </remarks>
        [HttpPost("generateDummy")]
        [ProducesResponseType(typeof(Result<IEnumerable<Patient>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateDummyDoctorsAsync()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                return BadRequest();

            var createdPatients = new List<Patient>();
            var dummyPatients = IdentityDbInitializer.GetDummyPatientUsers();

            foreach (var newUser in dummyPatients)
            {
                var userResult = await _userService.CreateUserAsync(newUser, IdentityConstants.DefaultPassword, Roles.Patient);

                if (userResult.IsSuccess)
                {
                    var patientResult = await _patientService.CreatePatientAsync(newUser);
                    createdPatients.Add(patientResult);
                }
            }

            return Ok(createdPatients);
        }
    }
}
