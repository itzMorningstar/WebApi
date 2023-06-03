using Entities.EmployeesEntities;

namespace ServicesLibrary.EmployeesServices
{
    public interface IEmployeeService
    {
        void Add(Employee employee);
        void Delete(int id);
        Employee Get(int id);
        List<Employee> GetAll();
        void Update(Employee employee);

        void AddSallery(Sallery sallery);

    }
}