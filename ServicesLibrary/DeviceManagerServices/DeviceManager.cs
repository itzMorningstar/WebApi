using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DeviceRegistrationEntity;

namespace ServicesLibrary
{
    public class DeviceManager : IDeviceManager
    {
        private readonly WebApiDatabase registeredDeviceContext;

        public DeviceManager(WebApiDatabase registeredDeviceContext)
        {
            this.registeredDeviceContext = registeredDeviceContext;
        }
        public async Task AddDevice(DeviceRegisterConnection deviceRegisterConnection)
        {
            if (deviceRegisterConnection != null)
            {
                var device = await registeredDeviceContext.DeviceRegisterConnections.FirstOrDefaultAsync(x => x.DeviceToken == deviceRegisterConnection.DeviceToken);
                if (device == null)
                {
                    await registeredDeviceContext.AddAsync(deviceRegisterConnection);
                }
                else
                {
                    device.UpdatedOn = DateTime.Now;
                }
                await registeredDeviceContext.SaveChangesAsync();
            }
        }

        public async Task<DeviceRegisterConnection> GetDeviceByUsername(string userName)
        {

            var device = await registeredDeviceContext.DeviceRegisterConnections.FirstOrDefaultAsync(x => x.UserName == userName);
            if (device != null)
            {
                return device;
            }
            return null;
        }

        public List<string> GetDeviceTokens(string userName)
        {
            var token = registeredDeviceContext.DeviceRegisterConnections.Where(x => x.UserName == userName).Select(x => x.DeviceToken).ToList();
            if (token != null)
            {
                return token;
            }
            return null;
        }

        public Task<DeviceRegisterConnection> UpdteDevice(DeviceRegisterConnection deviceRegisterConnection)
        {
            throw new NotImplementedException();
        }
    }
}
