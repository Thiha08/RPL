using RPL.Core.DTOs.Bookings;
using RPL.Core.Result;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Result<long>> CreateBookingAsync(long patientId, CreateBookingDto bookingDto);

        Task<Result<BookingInformationDto>> GetBookingInformationAsync(long patientId, long bookingId);

        Task<IResult> ConfirmBookingAsync(long patientId, ConfirmBookingDto clinicDto);

        Task<IResult> CancelBookingAsync(long patientId, CancelBookingDto cancelBookingDto);
    }
}
