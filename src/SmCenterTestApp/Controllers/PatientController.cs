using Microsoft.AspNetCore.Mvc;
using SmCenterTestApp.API.DTO;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO;
using SmCenterTestApp.Domain.Aggregates.PatientAggregate;
using Swashbuckle.AspNetCore.Annotations;

namespace SmCenterTestApp.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/patients/")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService patientService) => _service = patientService;

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(null, "Gets patient for change")]
        [SwaggerResponse(200, null, typeof(DoctorResponse))]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> GetPatientForChangeAsync(long id)
        {
            var doctor = await _service.GetPatientForChangeAsync(id);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok(doctor);
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(null, "Gets patients. Possible sort_by parameter values: first_name, middle_name, last_name, address, birthdate, sex, area")]
        [SwaggerResponse(200, null, typeof(IEnumerable<DoctorSimpleResponse>))]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> GetPatientsAsync(int page, int size, string? sort_by = null)
        {
            var doctors = await _service.GetPatientsAsync(page, size, sort_by);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok(doctors);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(null, "Creates patient")]
        [SwaggerResponse(200, "patient is created")]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> CreateAsync(PatientCreateRequest request)
        {
            await _service.CreateAsync(request.FirstName, request.MiddleName, request.LastName, request.Address, request.BirthDate, request.Sex, request.AreaId);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation(null, "Updates patient")]
        [SwaggerResponse(200, "patient is updated")]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> UpdateAsync(long id, PatientUpdateRequest request)
        {
            await _service.UpdateAsync(id, request.FirstName, request.MiddleName, request.LastName, request.Address, request.BirthDate, request.Sex, request.AreaId);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(null, "Deletes doctor")]
        [SwaggerResponse(200, "doctor is deleted")]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> DeleteAsync(long id)
        {
            await _service.DeleteAsync(id);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok();
        }
    }
}
