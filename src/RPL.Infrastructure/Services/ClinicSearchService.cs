using Ardalis.Result;
using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Interfaces;
using RPL.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class ClinicSearchService : IClinicSearchService
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public ClinicSearchService(
            IRepository<Clinic> clinicRepository,
            IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository)
        {
            _clinicRepository = clinicRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<IEnumerable<ClinicNearbyDto>>> GetAllNearbyClinicsAsync(ClinicNearbyRequestDto model)
        {
            throw new NotImplementedException();
        }
    }
}
