namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        
        
        private string GetDefaultMessageForStatusCode(in int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request, You have made",
                401 => "Authorized, You are not",
                404 => "Resource found, it is not",
                500 => "Errors are the path to the Dark side. Errors lead to anger, Anger leads to hate",
                _ => null
            };
        }
        
    }
}