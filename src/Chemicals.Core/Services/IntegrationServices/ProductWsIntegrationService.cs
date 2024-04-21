using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Interfaces.IntegrationServices;
using Chemicals.Core.Interfaces.Repositories;
using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Services.IntegrationServices;

public class ProductWsIntegrationService : IProductWsIntegrationService
{
    private readonly IReadRepository<ProductWarningSentence> _productWsReadRepository;

    public ProductWsIntegrationService(IReadRepository<ProductWarningSentence> productWsReadRepository)
    {
        _productWsReadRepository = productWsReadRepository;
    }

    public async Task<SharedProductWsDto> GetActiveWarningSentences()
    {
        var productWarningSentences = await _productWsReadRepository.ListAsync();

        var warningSentenceIds = productWarningSentences.Select(x => x.WarningSentenceId).ToList();

        return new SharedProductWsDto
        {
            WarningSentenceIds = warningSentenceIds
        };
    }
}