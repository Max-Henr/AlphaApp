using Business.Models;
using Domain.Models;

namespace Business.Services
{
    public interface IStatusService
    {
        Task<StatusResult<IEnumerable<Status>>> GetStatusAsync();
        Task<StatusResult<Status>> GetStatusByIdAsync(string id);
        Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
    }
}