using Manager_Device_Service.Domains.Data.User;

namespace Manager_Device_Service.Domains.Model.AccountRequest
{
    public class UpdateAccountRequestStatus
    {
        public int Id { get; set; }
        public AccountRequestStatus Status { get; set; }
    }
}
