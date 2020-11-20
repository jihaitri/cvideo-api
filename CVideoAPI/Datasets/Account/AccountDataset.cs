using CVideoAPI.Models;

namespace CVideoAPI.Datasets.Account
{
    public class AccountDataset
    {
        public int AccountId { get; set; }
        public string Created { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
