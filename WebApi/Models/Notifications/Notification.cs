using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Notifications
{
    public class Notification
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Message { get; set; }
        public string ToUsername { get; set; }
        public string FromUsername { get; set; }
    }
}
