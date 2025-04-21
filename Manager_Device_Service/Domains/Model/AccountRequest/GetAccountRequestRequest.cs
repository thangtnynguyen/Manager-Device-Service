using Manager_Device_Service.Core.Model;
using Manager_Device_Service.Domains.Data.User;

namespace Manager_Device_Service.Domains.Model.AccountRequest
{
    public class GetAccountRequestRequest : PagingRequest
    {
        public string? Keyword { get; set; }
        public AccountRequestStatus? Status { get; set; }
    }
}
