using CheckDrive.ApiContracts.Doctor;
using System;
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
                new DoctorDto {  Id = 3, Login = "user3", Password = "password3", PhoneNumber = "120456789", FirstName = "Azam", LastName = "Nazarov", Birthdate = new DateTime(1990, 1, 1)},
                new DoctorDto {  Id = 1, Login = "user1", Password = "password1", PhoneNumber = "123456789", FirstName = "John", LastName = "Doe", Birthdate = new DateTime(1990, 1, 1)},
                new DoctorDto {Id = 2, Login = "user2", Password = "password2", PhoneNumber = "987654321", FirstName = "Jane", LastName = "Siu", Birthdate = new DateTime(1995, 5, 15)},
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
