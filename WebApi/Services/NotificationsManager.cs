using WebApi.FirebaseIntegration;

namespace WebApi.Services
{
    public class NotificationsManager
    {
        public static async Task SendPushNotifications(string message, string fromUserName, List<string> tokens)
        {
            if (tokens.Any())
            {
                await Firebase.SendNotification
                    (
                        message,
                        fromUserName,
                        tokens
                    );
            }
        }
    }
}
