using AutoMapper;
using RPL.Core.Dtos;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Mappers
{
    public class AutomapperMaps : Profile
    {
        public AutomapperMaps()
        {
            CreateMap<Patient, PatientDto>().IncludeAllDerived().ReverseMap();
        }
    }
}
