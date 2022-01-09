using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.DTOs.Bookings;
using RPL.Core.Result;
using RPL.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace RPL.Ryawgen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class BookingsController : BaseController
    {
        private readonly IBookingService _bookingService;
        
        public BookingsController(
            IBookingService bookingService,
            IPatientService patientService
            ) : base(patientService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Create booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/bookings
        ///     {
        ///        "scheduleId": 3
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Result<BookingInformationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            long bookingId = await _bookingService.CreateBookingAsync(currentPatientInfo.PatientId, bookingDto);
            return Ok(await _bookingService.GetBookingInformationAsync(currentPatientInfo.PatientId, bookingId));
        }

        /// <summary>
        /// Get booking information
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/bookings/5/information/
        ///
        /// </remarks>
        [HttpGet("{id}/information")]
        [ProducesResponseType(typeof(Result<BookingInformationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingInformationAsync(long id)
        {
            return Ok(await _bookingService.GetBookingInformationAsync(currentPatientInfo.PatientId, id));
        }


        /// <summary>
        /// Confirm booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/bookings/5/confirm
        ///     {
        ///        "bookingId": 5,
        ///        "description": "This is Description for confirmation"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}/confirm")]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmBookingAsync(long id, ConfirmBookingDto confirmBookingDto)
        {
            if (id != confirmBookingDto.BookingId)
            {
                return Ok(Result.BadRequest());
            }

            return Ok(await _bookingService.ConfirmBookingAsync(currentPatientInfo.PatientId, confirmBookingDto));
        }


        /// <summary>
        /// Cancel booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/bookings/5/cancel
        ///     {
        ///        "bookingId": 5,
        ///        "Description": "This is Description for cancel"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}/cancel")]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelBookingAsync(long id, CancelBookingDto cancelBookingDto)
        {
            if (id != cancelBookingDto.BookingId)
            {
                return Ok(Result.BadRequest());
            }

            return Ok(await _bookingService.CancelBookingAsync(currentPatientInfo.PatientId, cancelBookingDto));
        }
    }
}
