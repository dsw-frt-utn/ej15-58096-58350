using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;


namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> _doctors = [];
        private List<Speciality> _specialities = [];
        public PersistenceInMemory()
        {
            LoadSpecialities();
        }
        private void LoadSpecialities()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Specialities.json");
                
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var loadedSpecialities = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                    if (loadedSpecialities != null)
                    {
                        _specialities.AddRange(loadedSpecialities);
                    }
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar especialidades: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Speciality>> GetAllSpecialities()
        {
            return _specialities;
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return _doctors.Where(d => d.IsActive);
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return _doctors.SingleOrDefault(d => d.Id == id && d.IsActive);
        }
        public async Task AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            var existing = _doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existing != null)
            {
                _doctors.Remove(existing);
                _doctors.Add(doctor);
            }
        }
    }    
}
