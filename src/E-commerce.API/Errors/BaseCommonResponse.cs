
namespace E_commerce.API.Errors
{
    public class BaseCommonResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public BaseCommonResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageForStatusCode(statusCode);
        }

        private string DefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status Code"
            };
        }

    }
}
