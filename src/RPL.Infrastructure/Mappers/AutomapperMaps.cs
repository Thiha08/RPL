using AutoMapper;
using RPL.Core.DTOs;
using RPL.Core.DTOs.Bookings;
using RPL.Core.DTOs.Doctors;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Mappers
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<Patient, PatientDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Clinic, ClinicDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Address, AddressDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Doctor, AvailableDoctorDto>();
            CreateMap<Doctor, DoctorDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Patient, PatientDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().IncludeAllDerived().ReverseMap();
        }
    }
}
