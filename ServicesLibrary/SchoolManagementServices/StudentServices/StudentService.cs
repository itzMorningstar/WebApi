using Entities.DeviceRegistrationEntity;
using Entities.SchoolManagement;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.SchoolManagementServices.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext webApiDatabase;
        private readonly IGenericRepository<Student> genericRepository;

        public StudentService(ApplicationDbContext webApiDatabase,IGenericRepository<Student> genericRepository)
        {
            this.webApiDatabase = webApiDatabase;
            this.genericRepository = genericRepository;
        }
        public void AddStudent(Student student)
        {
            genericRepository.Add(student);
        }

        public void DeleteStudent(int id)
        {
            genericRepository.Delete(id);
        }


        public Student GetStudentById(int id)
        {
           return genericRepository.GetById(id);
        }

        public void UpdateStudent(Student student)
        {
            genericRepository.Update(student);
        }

        Task<IEnumerable<Student>> IStudentService.GetAllStudents()
        {
            return Task.FromResult(genericRepository.GetAll());
        }
    }
}
