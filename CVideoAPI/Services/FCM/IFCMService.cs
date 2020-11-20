using System.Threading.Tasks;

namespace CVideoAPI.Services.FCM
{
    public interface IFCMService
    {
        Task<bool> CheckDevice(int? userId, string deviceId);
        Task SendMessage(int userId, string title, string body);
    }
}
