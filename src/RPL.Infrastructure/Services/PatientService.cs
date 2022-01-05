using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _repository;
        private readonly IMapper _mapper;

        public PatientService(IRepository<Patient> repository, IMapper mapper)
        {
            _repository = repository;
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

            return await _repository.AddAsync(newPatient);
        }
    }
}
