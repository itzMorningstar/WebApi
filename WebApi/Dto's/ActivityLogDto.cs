using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto_s
{
    public class ActivityLogDto
    {
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public int? AccountId { get; set; }
        public Guid? AccountGuid { get; set; }
        public int TypeId { get; set; }
    }
}
