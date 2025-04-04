using Domain.Models;

namespace Business.Models;

public class AppUserResult : ServiceResult
{
    public IEnumerable<AppUser>? Result { get; set; }
}
