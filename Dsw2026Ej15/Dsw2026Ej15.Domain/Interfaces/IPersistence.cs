using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<IEnumerable<Speciality>> GetAllSpecialities();
        Task<Speciality?> GetSpecialityById(Guid id);
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(Guid id);
        Task AddDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
    }
}
