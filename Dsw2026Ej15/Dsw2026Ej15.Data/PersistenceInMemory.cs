using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();
        private readonly List<Speciality> _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            // Acá irá la lectura de "products.json" o "specialities.json" de Persona B
            //te dejo este comentario 
        }

        public IEnumerable<Speciality> GetAllSpecialities() => _specialities;
        public Speciality GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);
        public IEnumerable<Doctor> GetAllDoctors() => _doctors;
        public Doctor GetDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id);

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var existing = GetDoctorById(doctor.Id);
            if (existing != null)
            {
                existing.Name = doctor.Name;
                existing.LicenseNumber = doctor.LicenseNumber;
                existing.IsActive = doctor.IsActive;
                existing.Speciality = doctor.Speciality;
            }
        }
    }
}
