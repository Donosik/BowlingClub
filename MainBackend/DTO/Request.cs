using RestSharp;

namespace MainBackend.DTO;

public class Request
{
    public Method HttpMethod { get; set; }
    public string Resource { get; set; }
    public object Body { get; set; }
}