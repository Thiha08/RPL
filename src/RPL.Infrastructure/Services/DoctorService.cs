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

        public async Task<Result<IEnumerable<AvailableDoctorDto>>> GetAvailableDoctorsAsync()
        {
            var doctorSpec = new AvailableDoctorsSpec();
            var availableDoctors = await _doctorRepository.ListAsync(doctorSpec);
            return Result<IEnumerable<AvailableDoctorDto>>.Ok(_mapper.Map<List<AvailableDoctorDto>>(availableDoctors));
        }

        public async Task<Result> UnassignFromClinicAsync(long id, long clinicId)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(id);
            Guard.Against.Null(doctor, nameof(doctor));
            doctor.ClinicId = 0;
            await _doctorRepository.UpdateAsync(doctor);
            return Result.Ok();
        }
    }
}
