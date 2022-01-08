using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Constants.Identity;
using RPL.Core.DTOs;
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
        /// Create Patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Patients
        ///     {
        ///        "Name": "MgMg",
        ///        "phoneNumber": "09424432855", // Phone number starts with '09'
        ///        "DateOfBirth": "1990-07-25T08:24:37.948Z",
        ///        "Address": {
        ///             "addressBody": "Thukha Rd, Yangon",
        ///             "latitude": "16.840216908631813",
        ///             "longitude": "96.1248596820284"
        ///         }
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Result<long>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePatientAsync(PatientDto patientDto)
        {
            var newUser = new ApplicationUser
            {
                UserName = patientDto.PhoneNumber,
                FullName = patientDto.Name,
                EmailConfirmed = true,
                PhoneNumber = patientDto.PhoneNumber,
                IsResetPasswordUponLoginNeeded = false,
                PhoneNumberConfirmed = true
            };
            var userResult = await _userService.CreateUserAsync(newUser, IdentityConstants.DefaultPassword, Roles.Doctor);

            if (!userResult.IsSuccess)
                throw new Exception(userResult.Message);

            patientDto.UserId = userResult.Data.Id;

            return Ok(await _patientService.CreatePatientAsync(patientDto));
        }


        /// <summary>
        /// Delete Patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/patients/5
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePaientAsync(long id)
        {
            return Ok(await _patientService.DeletePatientAsync(id));
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

        /// <summary>
        /// Get Patient by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Patients/5
        ///
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<PatientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientAsync(long id)
        {
            return Ok(await _patientService.GetPatientAsync(id));
        }

        /// <summary>
        /// Get Patients
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Patients
        ///
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<PatientDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientsAsync()
        {
            return Ok(await _patientService.GetPatientsAsync());
        }

        /// <summary>
        /// Update Patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Patients/5
        ///     {
        ///        "Name": "Thura",
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "DateOfBirth": "1990-01-20T08:24:37.948Z",
        ///        "Address": {
        ///             "addressBody": "Thukha Rd, Yangon",
        ///             "latitude": "16.840216908631813",
        ///             "longitude": "96.1248596820284"
        ///         }
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePatientAsync(long id, PatientDto patientDto)
        {
            return Ok(await _patientService.UpdatePatientAsync(id, patientDto));
        }

    }
}
