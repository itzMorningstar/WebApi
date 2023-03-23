//using Entities.Models;
using Entities.DeviceRegistrationEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary
{
    public interface IDeviceManager
    {
        Task<DeviceRegisterConnection> GetDeviceByUsername(string userName);

        List<string> GetDeviceTokens(string userName);

        Task AddDevice(DeviceRegisterConnection deviceRegisterConnection);

        Task<DeviceRegisterConnection> UpdteDevice(DeviceRegisterConnection deviceRegisterConnection);
    }
}
