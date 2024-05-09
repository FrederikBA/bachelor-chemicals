using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Interfaces.Integration;

public interface IWsHttpService
{
    public Task<List<SharedWarningSentenceDto>> GetActiveWarningSentenceAsync();
}