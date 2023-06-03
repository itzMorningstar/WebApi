using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ActivityLogging
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }

        [StringLength(250)]

        public string UserAgent { get; set; }

        public  int?  AccountId { get; set; }
        public  Guid?  AccountGuid { get; set; }
        public int TypeId { get; set; }

        //navigation properties
        public virtual ActivityType Type { get; set; }
        // Additional properties specific to the activity can be added here
    }
    public class ActivityType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }

        // Additional properties specific to activity types can be added here
    }
}
