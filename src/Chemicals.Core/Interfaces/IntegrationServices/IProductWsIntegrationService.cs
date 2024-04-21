using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Interfaces.IntegrationServices;

public interface IProductWsIntegrationService
{
    Task<SharedProductWsDto> GetActiveWarningSentences();
}