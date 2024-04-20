using System.Text.Json;
using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Exceptions;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Core.Models.Dtos;
using Chemicals.Core.Specifications;
using Shared.Integration.Authorization;
using Shared.Integration.Configuration;
using Shared.Integration.Models.Dtos;

namespace Chemicals.Core.Services;

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly IRepository<ProductWarningSentence> _productWarningSentenceRepository;
    private readonly HttpClient _httpClient;

    public ProductService(IReadRepository<Product> productReadRepository,
        IRepository<ProductWarningSentence> productWarningSentenceRepository)
    {
        _productReadRepository = productReadRepository;
        _productWarningSentenceRepository = productWarningSentenceRepository;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization",
            "Bearer " + IntegrationAuthService.GetIntegrationToken());
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _productReadRepository.ListAsync(new GetProductsFullSpec());

        if (products == null || products.Count == 0)
        {
            throw new ProductsNotFoundException();
        }

        return products;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _productReadRepository.FirstOrDefaultAsync(new GetProductByIdFullSpec(id));

        if (product == null)
        {
            throw new ProductNotFoundException(id);
        }

        return product;
    }

    public async Task<ProductWarningSentence> AddWarningSentenceAsync(AddWsDto dto)
    {
        var response = await _httpClient.GetAsync(Config.IntegrationEndpoints.WarningSentenceIntegration);
        var content = await response.Content.ReadAsStringAsync();
        var warningSentenceDtos = JsonSerializer.Deserialize<List<SharedWarningSentenceDto>>(content);

        //Validate if warning sentences exist
        if (warningSentenceDtos == null) throw new Exception("Warning Sentences not found.");

        //Validate if given warning sentence exist
        var itemIds = warningSentenceDtos!.Select(item => item.WarningSentenceId).ToList();
        var itemsExists = itemIds.Contains(dto.WarningSentenceId);

        if (!itemsExists) throw new Exception("Warning Sentence not found.");
        
        //Add warning sentence to product
        var productWarningSentence = new ProductWarningSentence
        {
            ProductId = 1,
            WarningSentenceId = warningSentenceDtos.First().WarningSentenceId
        };

        await _productWarningSentenceRepository.AddAsync(productWarningSentence);
        
        return productWarningSentence;
    }

    public Task<List<int>> GetProductWarningSentencesAsync(int productId)
    {
        throw new NotImplementedException();
    }
}