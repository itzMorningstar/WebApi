using Entities.DeviceRegistrationEntity;
using Entities.SchoolManagement;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.SchoolManagementServices.TeacherServices
{
    public class TeacherService : ITeacherService
    {
        private readonly WebApiDatabase webApiDatabase;
        private readonly IGenericRepository<Teacher> genericRepository;

        public TeacherService(WebApiDatabase webApiDatabase, IGenericRepository<Teacher> genericRepository)
        {
            this.webApiDatabase = webApiDatabase;
            this.genericRepository = genericRepository;
        }
        public void AddTeacher(Teacher teacher)
        {

            genericRepository.Add(teacher);
        }

        public void DeleteTeacher(int id)
        {
            genericRepository.Delete(id);
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return genericRepository.GetAll();
        }

        public Teacher GetTeacherById(int id)
        {
           return genericRepository.GetById(id);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            genericRepository.Update(teacher);
        }
    }
}
