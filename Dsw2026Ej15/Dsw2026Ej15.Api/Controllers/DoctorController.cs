using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] DoctorModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El nombre del médico es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("El número de licencia es requerido.");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
                throw new ValidationException("La especialidad indicada no existe.");

            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                IsActive = true,
                Speciality = speciality
            };

            _persistence.AddDoctor(newDoctor);

            return CreatedAtAction(nameof(GetById), new { id = newDoctor.Id }, newDoctor);
        }

        [HttpGet]
        public IActionResult GetAllActiveDoctors()
        {
            var allDoctors = _persistence.GetAllDoctors();
            var activeDoctors = allDoctors.Where(d => d.IsActive).ToList();
            return Ok(activeDoctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound(new { message = $"No se encontró un médico activo con el ID: {id}" });
            }

            var resultadoDto = new
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? "Sin especialidad"
            };

            return Ok(resultadoDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound(new { message = $"No se encontró un médico activo con el ID: {id} para dar de baja." });
            }

            doctor.IsActive = false;
            _persistence.UpdateDoctor(doctor);

            return NoContent();
        }


    }
}

