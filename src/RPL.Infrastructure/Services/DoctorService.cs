using Ardalis.GuardClauses;
using AutoMapper;
using RPL.Core.Entities;
using RPL.Core.Interfaces;
using RPL.Core.Result;
using RPL.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<Doctor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

            return await _repository.AddAsync(newDoctor);
        }
    }
}
