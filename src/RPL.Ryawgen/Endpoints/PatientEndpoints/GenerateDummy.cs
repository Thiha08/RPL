//using Ardalis.ApiEndpoints;
//using Microsoft.AspNetCore.Mvc;
//using RPL.Core.Constants.Identity;
//using RPL.Core.Entities;
//using RPL.Core.Interfaces;
//using RPL.Infrastructure.Data.Initializer;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RPL.Ryawgen.Endpoints.PatientEndpoints
//{
//    public class GenerateDummy : BaseAsyncEndpoint
//        .WithoutRequest
//        .WithoutResponse
//    {
//        private readonly IUserService _userService;
//        private readonly IPatientService _patientService;

//        public GenerateDummy(
//            IUserService userService,
//            IPatientService patientService)
//        {
//            _userService = userService;
//            _patientService = patientService;
//        }

//        [HttpPost("/GenerateDummyPatients")]
//        [Produces("application/json")]
//        [SwaggerOperation(
//            Summary = "Generate Dummy Patients",
//            Description = "Generate Dummy Patients for testing in development environment",
//            OperationId = "Patient.GenerateDummyPatients",
//            Tags = new[] { "PatientEndpoints" })
//        ]

//        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
//        {
//            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
//                return BadRequest();

//            var createdPatients = new List<Patient>();
//            var dummyPatients = IdentityDbInitializer.GetDummyPatientUsers();

//            foreach (var newUser in dummyPatients)
//            {
//                var userResult = await _userService.CreateUserAsync(newUser, IdentityConstants.DefaultPassword, Roles.Patient);

//                if (userResult.IsSuccess)
//                {
//                    var newPatient = new Patient
//                    {
//                        UserId = userResult.Value.Id,
//                        Name = userResult.Value.FullName,
//                        PhoneNumber = userResult.Value.PhoneNumber
//                    };

//                    var patientResult = await _patientService.CreatePatientAsync(newPatient);
//                    createdPatients.Add(patientResult);
//                }
//            }

//            return Ok(createdPatients);
//        }
//    }
//}
