using System.Net;
using MainBackend.DTO;
using Newtonsoft.Json;
using RestSharp;

namespace MainBackend.Helpers;

public abstract class GeneralRequestHelper
{
    protected RestClient client { get; set; }

    protected GeneralRequestHelper(string baseUrl)
    {
        client = new RestClient(baseUrl);
    }

    public Request CreateRequest(Method method, string resource, object body = null)
    {
        return new Request
        {
            HttpMethod = method,
            Resource = resource,
            Body = body
        };
    }

    public async Task<RestResponse> SendRequest(Request request)
    {
        var restRequest = new RestRequest(request.Resource, request.HttpMethod);
        if (request.Body != null)
            restRequest.AddJsonBody(JsonConvert.SerializeObject(request.Body));
        return await client.ExecuteAsync(restRequest);
    }
    
    public ApiResponse<U> ResponseOk<U>(string response)
    {
        var responseContent= JsonConvert.DeserializeObject<U>(response);
        return new ApiResponse<U>
        {
            IsSuccess = true,
            Content = responseContent
        };
    }
    public ApiResponse<U> ResponseUnauthorized<U>()
    {
        return new ApiResponse<U>
        {
            IsSuccess = false,
            ErrorMessage = "Unauthorized"
        };
    }
    
    public ApiResponse<U> ResponseBadRequest<U>()
    {
        return new ApiResponse<U>
        {
            IsSuccess = false,
            ErrorMessage = "Bad Request"
        };
    }
    
    public ApiResponse<U> ResponseNotFound<U>()
    {
        return new ApiResponse<U>
        {
            IsSuccess = false,
            ErrorMessage = "Not Found"
        };
    }
    public ApiResponse<U> ResponseInternalServerError<U>()
    {
        return new ApiResponse<U>
        {
            IsSuccess = false,
            ErrorMessage = "Internal Server Error"
        };
    }
    
    public ApiResponse<U> ResponseUnserialized<U>()
    {
        return new ApiResponse<U>
        {
            IsSuccess = false,
            ErrorMessage = "Unserialized"
        };
    }
    
    public ApiResponse<U> CheckResponse<U>(RestResponse response)
    {
        switch (response.StatusCode)
        {
            // HTTP 200
            case HttpStatusCode.OK:
                return ResponseOk<U>(response.Content);
                break;
            // HTTP 400
            case HttpStatusCode.BadRequest:
                return ResponseBadRequest<U>();
                break;
            // HTTP 401
            case HttpStatusCode.Unauthorized:
                return ResponseUnauthorized<U>();
                break;
            // HTTP 404
            case HttpStatusCode.NotFound:
                return ResponseNotFound<U>();
                break;
            // HTTP 500
            case HttpStatusCode.InternalServerError:
                return ResponseInternalServerError<U>();
                break;
            default:
                throw new Exception(response.Content);
                break;
        }
    }
}