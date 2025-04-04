using Domain.Models;

namespace Business.Models;

public abstract class ServiceResult
{
    public bool IsSuccess { get; set; } 
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
}
