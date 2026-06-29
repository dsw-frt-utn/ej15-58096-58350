using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf: IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Speciality>> GetAllSpecialities()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.Specialities.FindAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return _context.Doctors.Where(d => d.IsActive);
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id== id && d.IsActive);
        }

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

    }
}
