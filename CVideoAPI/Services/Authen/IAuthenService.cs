using CVideoAPI.Datasets.Account;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Authen
{
    public interface IAuthenService
    {
        Task<AccountDataset> Login(FirebaseToken userToken, string flg);
    }
}
