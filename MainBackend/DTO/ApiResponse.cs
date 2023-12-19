namespace MainBackend.DTO;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T Content { get; set; }
    public string ErrorMessage { get; set; }
}