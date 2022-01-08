using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs;
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
    public class ClinicsController : BaseController
    {
        private readonly IClinicService _clinicService;

        public ClinicsController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        /// <summary>
        /// Create Clinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Clinics
        ///     {
        ///        "clinicName": "Thukha Clinic",
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "clinicAddress": {
        ///             "addressBody": "Thukha Rd, Yangon",
        ///             "latitude": "16.840216908631813",
        ///             "longitude": "96.1248596820284"
        ///         }
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Result<ClinicDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateClinicAsync(ClinicDto clinicDto)
        {
            return Ok(await _clinicService.CreateClinicAsync(clinicDto));
        }

        /// <summary>
        /// Delete Clinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/Clinics/5
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClinicAsync(long id)
        {
            return Ok(await _clinicService.DeleteClinicAsync(id));
        }

        /// <summary>
        /// Get Clinic by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Clinics/5
        ///
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<ClinicDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClinicAsync(long id)
        {
            return Ok(await _clinicService.GetClinicAsync(id));
        }

        /// <summary>
        /// Get Clinics
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Clinics
        ///
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<ClinicDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClinicsAsync()
        {
            return Ok(await _clinicService.GetClinicsAsync());
        }

        /// <summary>
        /// Update Clinic
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Clinics/5
        ///     {
        ///        "clinicName": "Thukha Clinic",
        ///        "phoneNumber": "09424432870", // Phone number starts with '09'
        ///        "clinicAddress": {
        ///             "addressBody": "Thukha Rd, Yangon",
        ///             "latitude": "16.840216908631813",
        ///             "longitude": "96.1248596820284"
        ///         }
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClinicAsync(long id, ClinicDto clinicDto)
        {
            return Ok(await _clinicService.UpdateClinicAsync(id, clinicDto));
        }

    }
}
