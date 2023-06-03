
using Entities.DeviceRegistrationEntity;
using Entities.EmployeesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.EmployeesServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> genericRepository;
        private readonly WebApiDatabase webApiDatabase;
        private readonly IGenericRepository<Sallery> salleryRepository;

        public EmployeeService(IGenericRepository<Employee> genericRepository, WebApiDatabase webApiDatabase,IGenericRepository<Sallery> salleryRepository)
        {
            this.genericRepository = genericRepository;
            this.webApiDatabase = webApiDatabase;
            this.salleryRepository = salleryRepository;
        }
        public List<Employee> GetAll()
        {
            return webApiDatabase.Employees.Include(x => x.Sallery).ToList();

        }
        public Employee Get(int id)
        {
            return genericRepository.GetById(id);

        }

        public void Add(Employee employee)
        {
            genericRepository.Add(employee);
        }

        public void AddSallery(Sallery sallery)
        {
            salleryRepository.Add(sallery);
        }

        public void Delete(int id)
        {
            genericRepository.Delete(id);
        }
        public void Update(Employee employee)
        {
            genericRepository.Update(employee);
        }


    }
}
