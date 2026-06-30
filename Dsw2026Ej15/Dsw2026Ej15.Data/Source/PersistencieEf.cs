using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dsw2026Ej15.Data.Source
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Speciality> GetAllSpecialities()
            => _context.Specialities.ToList();

        public Speciality GetSpecialityById(Guid id)
            => _context.Specialities.FirstOrDefault(s => s.Id == id);

        public IEnumerable<Doctor> GetAllDoctors()
            => _context.Doctors
                .Include(d => d.Speciality)
                .ToList();

        public Doctor GetDoctorById(Guid id)
            => _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id);

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }
    }
}
