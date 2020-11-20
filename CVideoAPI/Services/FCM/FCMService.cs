using CVideoAPI.Models;
using CVideoAPI.Repositories;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVideoAPI.Services.FCM
{
    public class FCMService : IFCMService
    {
        private readonly IUnitOfWork _uow;
        public FCMService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<bool> CheckDevice(int? userId, string deviceId)
        {
            Models.UserDevice device = await _uow.UserDeviceRepository.GetFirst(filter: device => device.DeviceId == deviceId);
            Models.UserDevice newDevice = new Models.UserDevice()
            {
                AccountId = userId,
                DeviceId = deviceId
            };
            if (device == null)
            {
                _uow.UserDeviceRepository.Insert(newDevice);
            }
            else
            {
                device.AccountId = userId;
                _uow.UserDeviceRepository.Update(device);
            }
            return await _uow.CommitAsync() > 0;
        }

        public async Task SendMessage(int userId, string title, string body)
        {
            IEnumerable<UserDevice> userDevices = await _uow.UserDeviceRepository.Get(filter: device => device.AccountId == userId);
            List<UserDevice> list = new List<UserDevice>(userDevices);
            List<Message> messages = new List<Message>();
            if (list.Count > 0)
            {
                list.ForEach(device =>
                {
                    messages.Add(new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = title,
                            Body = body
                        },
                        Token = device.DeviceId
                    });
                });
                await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
            }
        }
    }
}
