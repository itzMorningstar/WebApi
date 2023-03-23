using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Dto_s
{
    public class DeviceRegisterationDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string DeviceToken { get; set; }
    }
}
