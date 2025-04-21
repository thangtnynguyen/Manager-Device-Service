using Manager_Device_Service.Core.Data;

namespace Manager_Device_Service.Domains.Data.University
{
    public class Building: EntityBase<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Description { get; set; }

        // Một tòa nhà có nhiều tầng
        public ICollection<Floor> Floors { get; set; } = new List<Floor>();

    }
}
