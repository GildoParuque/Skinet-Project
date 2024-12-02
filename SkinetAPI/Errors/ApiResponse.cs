
namespace SkinetAPI.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;

        Message = message ?? GetDefaulMessaForStatusCode(statusCode);
        
    }

    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    private string GetDefaulMessaForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it wa not",
            500 => "Errors are the path to the darkside. Errors lead to anger . Anger leads to hate. Hate leads to carrer change",
            _ => null
        };
    }
}
