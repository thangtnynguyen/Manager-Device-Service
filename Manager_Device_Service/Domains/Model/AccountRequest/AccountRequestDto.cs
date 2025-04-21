using Manager_Device_Service.Domains.Data.User;

namespace Manager_Device_Service.Domains.Model.AccountRequest
{
    public class AccountRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ClassCode { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public AccountRequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
