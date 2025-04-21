namespace Manager_Device_Service.Domains.Model.Device
{
    public class UpdateDeviceRequest
    {
        public int Id { get; set; }
        public int DeviceCategoryId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? RoomId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
    }

}
