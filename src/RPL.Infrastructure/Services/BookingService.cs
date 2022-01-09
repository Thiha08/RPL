using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.Constants;
using RPL.Core.DTOs.Bookings;
using RPL.Core.Entities;
using RPL.Core.Extensions;
using RPL.Core.Result;
using RPL.Core.Specifications.Bookings;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IReadRepository<DoctorSchedule> _scheduleRepository;
        private readonly IMapper _mapper;

        public BookingService(
            IRepository<Booking> bookingRepository,
            IReadRepository<DoctorSchedule> scheduleRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Result<long>> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            Guard.Against.Null(bookingDto, nameof(bookingDto));
            Booking booking = _mapper.Map<Booking>(bookingDto);
            booking.BookingNumber = 1; // hardcode for now
            booking.BookingStatus = BookingStatus.Pending;
            booking.DoctorScheduleId = bookingDto.ScheduleId;

            await _bookingRepository.AddAsync(booking);

            return booking.Id;

        }

        public async Task<Result<BookingInformationDto>> GetBookingInformationAsync(long bookingId)
        {
            Booking booking = await _bookingRepository.GetBySpecAsync(new BookingInformationSpec(bookingId));
            Guard.Against.Null(booking, nameof(booking));
            var bookingInfo = new BookingInformationDto();
            bookingInfo.Id = booking.Id;
            bookingInfo.BookingNumber = booking.BookingNumber;
            bookingInfo.ClinicName = booking.Clinic.ClinicName;
            bookingInfo.DoctorName = booking.Doctor.Name;

            var schedule = booking.Doctor.DoctorSchedule.FirstOrDefault();
            bookingInfo.Schedule = $"{schedule.ScheduleStartDateTime.ToTimeZoneTimeString("hh:mm tt")} ~ {schedule.ScheduleEndDateTime.ToTimeZoneTimeString("hh:mm tt")}";
            return bookingInfo;
        }

        public async Task<IResult> ConfirmBookingAsync(long id, ConfirmBookingDto confirmBookingDto)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(id);
            Guard.Against.Null(booking, nameof(booking));

            booking.Id = confirmBookingDto.BookingId;
            booking.Description = confirmBookingDto.Description;
            booking.BookingStatus = BookingStatus.Confirmed;
            await _bookingRepository.UpdateAsync(booking);
            return Result.Ok();
        }

        public async Task<IResult> CancelBookingAsync(long id, CancelBookingDto cancelBookingDto)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(id);
            Guard.Against.Null(booking, nameof(booking));

            booking.Id = cancelBookingDto.BookingId;
            booking.Description = cancelBookingDto.Description;
            booking.BookingStatus = BookingStatus.Cancelled;

            await _bookingRepository.UpdateAsync(booking);
            return Result.Ok();
        }
    }
}
