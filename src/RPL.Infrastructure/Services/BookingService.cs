using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.Constants;
using RPL.Core.DTOs.Bookings;
using RPL.Core.Entities;
using RPL.Core.Extensions;
using RPL.Core.Result;
using RPL.Core.Specifications.Bookings;
using RPL.Core.Specifications.Doctors;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IReadRepository<DoctorSchedule> _scheduleRepository;
        private readonly IReadRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public BookingService(
            IRepository<Booking> bookingRepository,
            IReadRepository<DoctorSchedule> scheduleRepository,
            IReadRepository<Doctor> doctorRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _scheduleRepository = scheduleRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> CreateBookingAsync(long patientId, CreateBookingDto bookingDto)
        {
            Guard.Against.Null(bookingDto, nameof(bookingDto));

            Doctor doctor = await _doctorRepository.GetBySpecAsync(new DoctorByScheduleIdSpec(bookingDto.ScheduleId));

            Guard.Against.Null(doctor, nameof(doctor));

            var booking = new Booking
            {
                BookingNumber = 1, // hardcode for now
                BookingStatus = BookingStatus.Pending,
                PatientId = patientId,
                DoctorScheduleId = bookingDto.ScheduleId,
                ClinicId = doctor.ClinicId.Value,
                DoctorId = doctor.Id
            };

            await _bookingRepository.AddAsync(booking);
            return booking.Id;
        }

        public async Task<Result<BookingInformationDto>> GetBookingInformationAsync(long patientId, long bookingId)
        {
            Booking booking = await _bookingRepository.GetBySpecAsync(new BookingInformationSpec(patientId, bookingId));

            Guard.Against.Null(booking, nameof(booking));

            DoctorSchedule schedule = booking.Doctor.DoctorSchedule.FirstOrDefault();

            Guard.Against.Null(schedule, nameof(schedule));

            var bookingInfo = new BookingInformationDto
            {
                Id = booking.Id,
                BookingNumber = booking.BookingNumber,
                ClinicName = booking.Clinic.ClinicName,
                DoctorName = booking.Doctor.Name,
                Schedule = $"{schedule.ScheduleStartDateTime.ToTimeZoneTimeString("hh:mm tt")} ~ {schedule.ScheduleEndDateTime.ToTimeZoneTimeString("hh:mm tt")}"
            };

            return bookingInfo;
        }

        public async Task<IResult> ConfirmBookingAsync(long patientId, ConfirmBookingDto confirmBookingDto)
        {
            Booking booking = await _bookingRepository.GetBySpecAsync(new PatientBookingByIdSpec(patientId, confirmBookingDto.BookingId));

            Guard.Against.Null(booking, nameof(booking));

            booking.Description = confirmBookingDto.Description;
            booking.BookingStatus = BookingStatus.Confirmed;
            
            await _bookingRepository.UpdateAsync(booking);
            return Result.Ok();
        }

        public async Task<IResult> CancelBookingAsync(long patientId, CancelBookingDto cancelBookingDto)
        {
            Booking booking = await _bookingRepository.GetBySpecAsync(new PatientBookingByIdSpec(patientId, cancelBookingDto.BookingId));
            
            Guard.Against.Null(booking, nameof(booking));

            booking.Description = cancelBookingDto.Description;
            booking.BookingStatus = BookingStatus.Cancelled;

            await _bookingRepository.UpdateAsync(booking);
            return Result.Ok();
        }
    }
}
