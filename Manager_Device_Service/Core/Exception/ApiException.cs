using Manager_Device_Service.Core.Constant;

namespace Manager_Device_Service.Core.Exception
{
    public class ApiException : System.Exception // Explicitly specify System.Exception  
    {
        public int Status { get; }
        public object Data { get; }

        public ApiException(string message, int status = HttpStatusCodeConstant.BadRequest, object data = null) : base(message)
        {
            Status = status;
            Data = data;
        }
    }
}
