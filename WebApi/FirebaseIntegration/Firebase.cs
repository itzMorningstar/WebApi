using Microsoft.AspNetCore.Builder.Extensions;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.FirebaseIntegration
{
    public class Firebase
    {
        public static async Task SendNotification(string body, string title, List<string> DeviceTokens)
        {
            InitializeFirebaseApp();

            var message = new MulticastMessage()
            {
                Notification = new Notification
                {
                    Body = body,
                    Title = title,
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Badge = 0,
                        Sound = "",
                        ContentAvailable = true,
                        MutableContent = true,
                    }
                },
                //have to replace the link with the actual link of the website this is to configure the on click event in the web notification.
                Webpush = new WebpushConfig
                {
                    Notification = new WebpushNotification
                    {
                        Body = body,
                        Title = title
                    },
                    FcmOptions = new WebpushFcmOptions
                    {
                        Link = "https://www.google.com"
                    }
                },
                Tokens = DeviceTokens
            };

            await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);

            // When user open application in normal flow without clicking on pushnotification, getIntent().hasExtra(“pushnotification”) is null so command goes to catch block and CheckLogin() method checked for already login or not.
            // But user entered by clicking pushnotification then getIntent().hasExtra(“pushnotification”) is not null and he will go to desired Activity.
            // Will add the data block later after we willknow where do we have to redirect the user if he click on the message and might to redesign the 
            // notification model based on that.x
        }
        private static void InitializeFirebaseApp()
        {
            try
            {
                var defaultApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(AppDomain.CurrentDomain.BaseDirectory + "FirebaseApiConfiguration.json")
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
