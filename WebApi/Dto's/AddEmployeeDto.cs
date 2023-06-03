using Entities.Enums;

namespace WebApi.Dto_s
{
    public class AddEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Age { get; set; }
        public Department Department { get; set; }
        public decimal SalleryAmount { get; set; }
        public Gender Gender { get; set; }
    }
}
