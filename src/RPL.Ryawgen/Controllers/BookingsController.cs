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
    [AllowAnonymous]
    public class BookingsController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Create Booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/bookings
        ///     {
        ///        "clinicId": 1,
        ///        "scheduleId": 3,
        ///        "doctorId": 4,
        ///        "patientId": 1
        ///        
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Result<BookingInformationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            long bookingId = await _bookingService.CreateBookingAsync(bookingDto);
            return Ok(await _bookingService.GetBookingInformationAsync(bookingId));
        }

        /// <summary>
        /// Get Booking Information
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/bookings/information/5
        ///
        /// </remarks>
        [HttpGet("information/{id}")]
        [ProducesResponseType(typeof(Result<BookingInformationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingInformationAsync(long id)
        {
            return Ok(await _bookingService.GetBookingInformationAsync(id));
        }


        /// <summary>
        /// Confirm Booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/bookings/confirm/5
        ///     {
        ///        "bookingId": 5,
        ///        "Description": "This is Description for confirmation", 
        ///        
        ///     }
        ///
        /// </remarks>
        [HttpPut("confirm/{id}")]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmBookingAsync(long id, ConfirmBookingDto confirmBookingDto)
        {
            if (id != confirmBookingDto.BookingId)
            {
                return Ok(Result.BadRequest());
            }

            return Ok(await _bookingService.ConfirmBookingAsync(id, confirmBookingDto));
        }


        /// <summary>
        /// Cancel Booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/bookings/cancel/5
        ///     {
        ///        "bookingId": 5,
        ///        "Description": "This is Description for cancel", 
        ///        
        ///     }
        ///
        /// </remarks>
        [HttpPut("cancel/{id}")]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelBookingAsync(long id, CancelBookingDto cancelBookingDto)
        {
            if (id != cancelBookingDto.BookingId)
            {
                return Ok(Result.BadRequest());
            }

            return Ok(await _bookingService.CancelBookingAsync(id, cancelBookingDto));
        }
    }
}
