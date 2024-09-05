using Microsoft.AspNetCore.Mvc;
using SmCenterTestApp.API.DTO;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace SmCenterTestApp.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/doctors/")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService doctorService) => _service = doctorService;

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(null, "Gets doctor for change")]
        [SwaggerResponse(200, null, typeof(DoctorResponse))]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> GetDoctorForChangeAsync(long id)
        {
            var doctor = await _service.GetDoctorForChangeAsync(id);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok(doctor);
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(null, "Gets doctors. Possible sort_by parameter values: first_name, middle_name, last_name, room, area, specialization")]
        [SwaggerResponse(200, null, typeof(IEnumerable<DoctorSimpleResponse>))]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> GetDoctorsAsync(int page, int size, string? sort_by = null)
        {
            var doctors = await _service.GetDoctorsAsync(page, size, sort_by);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok(doctors);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(null, "Creates doctor")]
        [SwaggerResponse(200, "doctor is created")]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> CreateAsync(DoctorCreateRequest request)
        {
            await _service.CreateAsync(request.FirstName, request.MiddleName, request.LastName, request.RoomId, request.SpecializationId, request.AreaId);

            if (!_service.ValidationState.IsValid) return Results.Conflict(_service.ValidationState.Errors);

            return Results.Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation(null, "Updates doctor")]
        [SwaggerResponse(200, "doctor is updated")]
        [SwaggerResponse(409, "errors", typeof(IEnumerable<string>))]
        public async Task<IResult> UpdateAsync(long id, DoctorUpdateRequest request)
        {
            await _service.UpdateAsync(id, request.FirstName, request.MiddleName, request.LastName, request.RoomId, request.SpecializationId, request.AreaId);

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
