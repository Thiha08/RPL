using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.DTOs.Doctors;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Core.Specifications.Doctors;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<Result> AssignToClinicAsync(long id, long clinicId)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));
            doctor.ClinicId = clinicId;
            await _doctorRepository.UpdateAsync(doctor);
            return Result.Ok();
        }

        public async Task<Result<Doctor>> CreateDoctorAsync(ApplicationUser model)
        {
            Guard.Against.Null(model, nameof(model));

            var newDoctor = new Doctor
            {
                UserId = model.Id,
                Name = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            return await _doctorRepository.AddAsync(newDoctor);
        }

        public async Task<Result<long>> CreateDoctorAsync(DoctorDto doctorDto)
        {
            Guard.Against.Null(doctorDto, nameof(doctorDto));
            Doctor doctor = await _doctorRepository.AddAsync(_mapper.Map<Doctor>(doctorDto));
            return Result<long>.Ok(doctor.Id);
        }

        public async Task<IResult> DeleteDoctorAsync(long id)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));
            doctor.Status = false;
            await _doctorRepository.UpdateAsync(doctor);
            return Result.Ok();
        }

        public async Task<Result<IEnumerable<AvailableDoctorDto>>> GetAvailableDoctorsAsync()
        {
            var doctorSpec = new AvailableDoctorsSpec();
            var availableDoctors = await _doctorRepository.ListAsync(doctorSpec);
            return Result<IEnumerable<AvailableDoctorDto>>.Ok(_mapper.Map<List<AvailableDoctorDto>>(availableDoctors));
        }

        public async Task<Result<DoctorDto>> GetDoctorAsync(long id)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));
            return Result<DoctorDto>.Ok(_mapper.Map<DoctorDto>(doctor));
        }


        public async Task<Result<IEnumerable<DoctorDto>>> GetDoctorsAsync()
        {
            List<Doctor> doctors = await _doctorRepository.ListAsync();
            return Result<IEnumerable<DoctorDto>>.Ok(_mapper.Map<List<DoctorDto>>(doctors));
        }


        public async Task<Result> UnassignFromClinicAsync(long id, long clinicId)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));
            doctor.ClinicId = 0;
            await _doctorRepository.UpdateAsync(doctor);
            return Result.Ok();
        }

        public async Task<IResult> UpdateDoctorAsync(long id, DoctorDto doctorDto)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));

            doctor.Name = doctorDto.Name;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.DateOfBirth = doctorDto.DateOfBirth;
           
            doctor.Address = new Address
            {
                AddressBody = doctorDto.Address?.AddressBody,
                Latitude = doctorDto.Address?.Latitude ?? 0,
                Longitude = doctorDto.Address?.Longitude ?? 0,
            };
            await _doctorRepository.UpdateAsync(doctor);
            return Result.Ok();
        }
    }
}
