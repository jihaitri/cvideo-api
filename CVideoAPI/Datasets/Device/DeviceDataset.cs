using System.ComponentModel.DataAnnotations;

namespace CVideoAPI.Datasets.Device
{
    public class DeviceDataset
    {
        [Required]
        public string DeviceId { get; set; }
    }
}
