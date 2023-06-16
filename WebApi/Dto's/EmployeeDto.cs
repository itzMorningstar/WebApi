using Entities.EmployeesEntities;
using Entities.Enums;

namespace WebApi.Dto_s
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Age { get; set; }
        public string Department { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Gender Gender { get; set; }
       public string Sallery { get; set; }
    }
}
