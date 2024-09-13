using Microsoft.AspNetCore.Http.HttpResults;

namespace ZimoziAssesment.Error
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public Response(int statusCode, T data , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
            Data = data;
        }
        public string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request , you have made !",
                404 => "Resources Not Found",
                500 => "This is server error",
                _ => null
            };
        }
    }
}
