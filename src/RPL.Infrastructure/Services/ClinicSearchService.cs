using RPL.Core.DTOs;
using RPL.Core.Entities;
using RPL.Core.Extensions;
using RPL.Core.Filters;
using RPL.Core.Result;
using RPL.Core.Specifications;
using RPL.Core.Specifications.Doctors;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class ClinicSearchService : IClinicSearchService
    {
        private readonly IReadRepository<Clinic> _clinicRepository;
        private readonly IReadRepository<DoctorSchedule> _scheduleRepository;

        public ClinicSearchService(
            IReadRepository<Clinic> clinicRepository,
            IReadRepository<DoctorSchedule> scheduleRepository)
        {
            _clinicRepository = clinicRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Result<IEnumerable<ClinicNearbyDto>>> GetNearbyClinicsAsync(ClinicNearbyFilter filter)
        {
            var clinicSpec = new ClinicsNearbySearchSpec(filter);
            var clinicsNearby = await _clinicRepository.ListAsync(clinicSpec);

            var doctors = clinicsNearby.SelectMany(x => x.Doctors).ToList();

            var scheduleFilter = new DoctorScheduleFilter
            {
                DoctorIds = doctors.Select(x => x.Id).ToList(),
                StartDateTime = DateTime.UtcNow.Date.AddDays(-1),
                EndDateTime = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1)
            };
            var scheduleSpec = new DoctorSchedulesByDateSpec(scheduleFilter);
            List<DoctorSchedule> schedules = await _scheduleRepository.ListAsync(scheduleSpec);

            var clinicsNearbyDto = new List<ClinicNearbyDto>();

            foreach (var clinic in clinicsNearby)
            {
                var clinicDoctors = doctors.Where(x => x.ClinicId == clinic.Id).ToList();
                var clinicDoctorIds = clinicDoctors.Select(x => x.Id).ToList();

                var clinics = schedules.Where(x => clinicDoctorIds.Contains(x.DoctorId))
                    .Select(x => new ClinicNearbyDto
                    {
                        ClinicId = clinic.Id,
                        DoctorId = x.DoctorId,
                        ScheduleId = x.Id,
                        ClinicName = clinic.ClinicName,
                        DoctorName = clinicDoctors.FirstOrDefault(d => d.Id == x.DoctorId)?.Name,
                        Address = clinic.ClinicAddress.AddressBody,
                        Schedule = $"{x.ScheduleStartDateTime.ToTimeZoneTimeString("hh:mm tt")} ~ {x.ScheduleEndDateTime.ToTimeZoneTimeString("hh:mm tt")}",
                        ScheduleStartDateTime = x.ScheduleStartDateTime.ToTimeZoneTime(),
                        ScheduleEndDateTime = x.ScheduleEndDateTime.ToTimeZoneTime()
                    }).ToList();

                clinicsNearbyDto.AddRange(clinics);
            }

            return clinicsNearbyDto;
        }
    }
}
