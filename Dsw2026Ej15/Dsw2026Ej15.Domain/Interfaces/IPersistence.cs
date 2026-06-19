using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        IEnumerable<Speciality> GetAllSpecialities();
        Speciality? GetSpecialityById(Guid id);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctorById(Guid id);
        void AddDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
    }
}
