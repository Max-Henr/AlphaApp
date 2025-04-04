using Business.Models;
using Data.Interfaces;
using Domain.Models;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult<IEnumerable<Status>>> GetStatusAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        return result.IsSuccess
            ? new StatusResult<IEnumerable<Status>> { IsSuccess = true, StatusCode = 200, Result = result.Result }
            : new StatusResult<IEnumerable<Status>> { IsSuccess = false, StatusCode = 500, ErrorMessage = "Error getting statuses" };
    }

    public async Task<StatusResult<Status>> GetStatusByNameAsync(string statusName)
    {
        var result = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        return result.IsSuccess
            ? new StatusResult<Status> { IsSuccess = true, StatusCode = 200, Result = result.Result }
            : new StatusResult<Status> { IsSuccess = false, StatusCode = 500, ErrorMessage = "Error getting statuses" };
    }

    public async Task<StatusResult<Status>> GetStatusByIdAsync(string id)
    {
        var result = await _statusRepository.GetAsync(x => x.Id == id);
        return result.IsSuccess
            ? new StatusResult<Status> { IsSuccess = true, StatusCode = 200, Result = result.Result }
            : new StatusResult<Status> { IsSuccess = false, StatusCode = 500, ErrorMessage = "Error getting statuses" };
    }
}