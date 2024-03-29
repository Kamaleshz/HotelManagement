namespace UserManagement.Models.DTO
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string? Error { get; set; }
        public Response(T data)
        {
            Success = true;
            Data = data;
        }

        public Response(T data, string Message)
        {
            Success = false;
            Data = data;
            Error = Message;
        }
    }
}
