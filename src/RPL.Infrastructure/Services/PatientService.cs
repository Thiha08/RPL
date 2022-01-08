using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IRepository<Patient> repository, IMapper mapper)
        {
            _patientRepository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Patient>> CreatePatientAsync(ApplicationUser model)
        {
            Guard.Against.Null(model, nameof(model));

            var newPatient = new Patient
            {
                UserId = model.Id,
                Name = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            return await _patientRepository.AddAsync(newPatient);
        }

        public async Task<Result<long>> CreatePatientAsync(PatientDto patientDto)
        {
            Guard.Against.Null(patientDto, nameof(patientDto));
            Patient patient = await _patientRepository.AddAsync(_mapper.Map<Patient>(patientDto));
            return Result<long>.Ok(patient.Id);
        }

        public async Task<IResult> DeletePatientAsync(long id)
        {
            Patient patient = await _patientRepository.GetByIdAsync(id);
            Guard.Against.Null(patient, nameof(patient));
            patient.Status = false;
            await _patientRepository.UpdateAsync(patient);
            return Result.Ok();
        }

        public async Task<Result<PatientDto>> GetPatientAsync(long id)
        {
            Patient patient = await _patientRepository.GetByIdAsync(id);
            Guard.Against.Null(patient, nameof(patient));
            return Result<PatientDto>.Ok(_mapper.Map<PatientDto>(patient));
        }


        public async Task<Result<IEnumerable<PatientDto>>> GetPatientsAsync()
        {
            List<Patient> patients = await _patientRepository.ListAsync();
            return Result<IEnumerable<PatientDto>>.Ok(_mapper.Map<List<PatientDto>>(patients));
        }

        public async Task<IResult> UpdatePatientAsync(long id, PatientDto patientDto)
        {
            Patient patient = await _patientRepository.GetByIdAsync(id);
            Guard.Against.Null(patient, nameof(patient));

            patient.Name = patientDto.Name;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.DateOfBirth = patientDto.DateOfBirth;

            patient.Address = new Address
            {
                AddressBody = patientDto.Address?.AddressBody,
                Latitude = patientDto.Address?.Latitude ?? 0,
                Longitude = patientDto.Address?.Longitude ?? 0,
            };
            await _patientRepository.UpdateAsync(patient);
            return Result.Ok();
        }
    }
}
