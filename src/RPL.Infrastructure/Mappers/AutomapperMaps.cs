using AutoMapper;
using RPL.Core.DTOs;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Mappers
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<Patient, PatientDto>().IncludeAllDerived().ReverseMap();
            CreateMap<Clinic, ClinicDto>().IncludeAllDerived().ReverseMap();
        }
    }
}
