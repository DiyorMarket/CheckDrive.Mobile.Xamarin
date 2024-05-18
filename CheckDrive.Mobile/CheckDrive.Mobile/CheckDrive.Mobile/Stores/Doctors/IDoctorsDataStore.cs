using CheckDrive.DTOs.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CheckDrive.Web.Stores.Doctors
{
    public interface IDoctorDataStore
    {
        Task<List<DoctorDto>> GetDoctors();
        Task<DoctorDto> GetDoctor(int id);
        Task<DoctorDto> CreateDoctor(DoctorDto doctor);
        Task<DoctorDto> UpdateDoctor(int id, DoctorDto doctor);
        Task DeleteDoctor(int id);
    }
}
