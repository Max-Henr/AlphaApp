using Domain.Models;

namespace Business.Models;

public class ProjectResult<T> : ServiceResult
{
    public IEnumerable<T>? Result { get; set; }
}
public class ProjectResult : ServiceResult
{
}
