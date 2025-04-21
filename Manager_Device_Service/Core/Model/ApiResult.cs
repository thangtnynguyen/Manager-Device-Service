namespace Manager_Device_Service.Core.Model
{

    public class ApiResult<T>
    {
        public bool Status { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public ApiResult(bool Status, string Message, T Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;

        }
        public ApiResult()
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }

        public ApiResult(string Message, T Data)
        {
            this.Status = true;
            this.Message = Message;
            this.Data = Data;
        }
        // Phương thức tiện ích để trả về kết quả thành công
        public static ApiResult<T> ReusltSuccessCode(int? resultCode, string? message = null, T? data = default)
        {
            if (!resultCode.HasValue)
            {
                return new ApiResult<T>(false, message ?? "Không tìm thấy đối tượng.", data);

            }
            if (resultCode == 0)
            {
                return new ApiResult<T>(true, message ?? "Hoạt động đã thành công.", data);
            }
            else if (resultCode == 1)
            {
                return new ApiResult<T>(false, message ?? "Không tìm thấy đối tượng.", data);

            }
            else if (resultCode == 2)
            {
                return new ApiResult<T>(false, message ?? "Không tìm thấy đối tượng.", data);

            }
            return new ApiResult<T>(false, message ?? "Hoạt động thất bại.", data);

        }

        // Phương thức tiện ích để trả về kết quả thành công
        public static ApiResult<T> Success(string? message = null, T? data = default)
        {
            return new ApiResult<T>(true, message ?? "Hoạt động đã thành công.", data);
        }

        // Phương thức tiện ích để trả về kết quả thất bại
        public static ApiResult<T> Failure(string message, T? data = default)
        {
            return new ApiResult<T>(false, message, data);
        }


        public static ApiResult<T> LogSuccess(T data, bool status)
        {
            return new ApiResult<T> { Data = data, Status = status, Message = null };
        }


    }

}
