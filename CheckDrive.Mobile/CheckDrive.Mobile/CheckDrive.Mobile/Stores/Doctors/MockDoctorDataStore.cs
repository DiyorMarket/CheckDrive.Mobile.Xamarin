using CheckDrive.DTOs.Doctor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckDrive.Web.Stores.Doctors
{
    public class MockDoctorDataStore : IDoctorDataStore
    {
        private readonly List<DoctorDto> _doctors;

        public MockDoctorDataStore()
        {
            _doctors = new List<DoctorDto>
            {
                new DoctorDto { Id = 1, AccountId = 1 },
                new DoctorDto { Id = 2, AccountId = 2 },
            };
        }

        public async Task<List<DoctorDto>> GetDoctors()
        {
            await Task.Delay(100);
            return _doctors.ToList();
        }

        public async Task<DoctorDto> GetDoctor(int id)
        {
            await Task.Delay(100);
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public async Task<DoctorDto> CreateDoctor(DoctorDto doctor)
        {
            await Task.Delay(100);
            doctor.Id = _doctors.Max(d => d.Id) + 1;
            _doctors.Add(doctor);
            return doctor;
        }

        public async Task<DoctorDto> UpdateDoctor(int id, DoctorDto doctor)
        {
            await Task.Delay(100);
            var existingDoctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (existingDoctor != null)
            {
                existingDoctor.AccountId = doctor.AccountId;
            }
            return existingDoctor;
        }

        public async Task DeleteDoctor(int id)
        {
            await Task.Delay(100);
            var doctorToRemove = _doctors.FirstOrDefault(d => d.Id == id);
            if (doctorToRemove != null)
            {
                _doctors.Remove(doctorToRemove);
            }
        }
    }
}
