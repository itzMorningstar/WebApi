using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DeviceRegistrationEntity
{
    public class DeviceRegisterConnection
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string DeviceToken { get; set; } = null!;

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool? Status { get; set; }
    }
}
