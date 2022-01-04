using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Infrastructure.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IMapper _mapper;

        public ClinicService(IRepository<Clinic> clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> CreateClinicAsync(ClinicDto clinicDto)
        {
            Guard.Against.Null(clinicDto, nameof(clinicDto));
            Clinic clinic = await _clinicRepository.AddAsync(_mapper.Map<Clinic>(clinicDto));
            return Result<long>.Ok(clinic.Id);
        }

        public async Task<IResult> DeleteClinicAsync(long id)
        {
            Clinic clinic = await _clinicRepository.GetByIdAsync(id);
            Guard.Against.Null(clinic, nameof(clinic));
            clinic.Status = false;
            await _clinicRepository.UpdateAsync(clinic);
            return Result.Ok();
        }

        public async Task<Result<ClinicDto>> GetClinicAsync(long id)
        {
            Clinic clinic = await _clinicRepository.GetByIdAsync(id);
            Guard.Against.Null(clinic, nameof(clinic));
            return Result<ClinicDto>.Ok(_mapper.Map<ClinicDto>(clinic));
        }

        public async Task<Result<IEnumerable<ClinicDto>>> GetClinicsAsync()
        {
            List<Clinic> clinics = await _clinicRepository.ListAsync();
            return Result<IEnumerable<ClinicDto>>.Ok(_mapper.Map<List<ClinicDto>>(clinics));
        }

        public async Task<IResult> UpdateClinicAsync(long id, ClinicDto clinicDto)
        {
            Clinic clinic = await _clinicRepository.GetByIdAsync(id);
            Guard.Against.Null(clinic, nameof(clinic));
            await _clinicRepository.UpdateAsync(_mapper.Map<Clinic>(clinicDto));
            return Result.Ok();
        }
    }
}
