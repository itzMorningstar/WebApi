using Entities.SchoolManagement;
using ServicesLibrary.GenericRepositories;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.SchoolManagementServices.Enrollmentservices
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IGenericRepository<Enrollment> genericRepository;

        public EnrollmentService(IGenericRepository<Enrollment> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public void AddEnrollment(Enrollment enrollment)
        {
            genericRepository.Add(enrollment);
        }

        public void DeleteEnrollment(int id)
        {
            genericRepository.Delete(id);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return genericRepository.GetAll();
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return genericRepository.GetById(id);
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            genericRepository.Update(enrollment);
        }
    }
}
