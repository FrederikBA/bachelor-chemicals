using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Interfaces.Integration;

public interface IProductWsIntegrationService
{
    Task<SharedProductWsDto> GetActiveWarningSentences();
}