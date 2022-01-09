using RPL.Core.DTOs.Bookings;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Result<long>> CreateBookingAsync(CreateBookingDto bookingDto);

        Task<Result<BookingInformationDto>> GetBookingInformationAsync(long bookingId);

        Task<IResult> ConfirmBookingAsync(long id, ConfirmBookingDto clinicDto);

        Task<IResult> CancelBookingAsync(long id, CancelBookingDto cancelBookingDto);
    }
}
