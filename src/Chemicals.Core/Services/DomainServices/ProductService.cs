using System.Text.Json;
using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Exceptions;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Integration;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Core.Models.Dtos;
using Chemicals.Core.Specifications;
using Microsoft.Extensions.Logging;
using Shared.Integration.Authorization;
using Shared.Integration.Configuration;
using Shared.Integration.Models.Dtos;
using Shared.Integration.Models.Dtos.Sync;

namespace Chemicals.Core.Services.DomainServices;

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly IRepository<ProductWarningSentence> _productWarningSentenceRepository;
    private readonly ISyncProducer _syncProducer;
    private readonly IWsHttpService _wsHttpService;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IReadRepository<Product> productReadRepository,
        IRepository<ProductWarningSentence> productWarningSentenceRepository, ISyncProducer syncProducer,
        ILogger<ProductService> logger, IWsHttpService wsHttpService)
    {
        _productReadRepository = productReadRepository;
        _productWarningSentenceRepository = productWarningSentenceRepository;
        _syncProducer = syncProducer;
        _logger = logger;
        _wsHttpService = wsHttpService;
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
        //Get warning sentences from integration endpoint
        var warningSentenceDtos = await _wsHttpService.GetActiveWarningSentenceAsync();
        
        //Validate if warning sentences exist
        if (warningSentenceDtos == null)
        {
            _logger.LogError("No warning sentences found from integration endpoint.");
            throw new Exception("Warning Sentences not found.");
        }

        //Validate if given warning sentence exist
        var itemIds = warningSentenceDtos!.Select(item => item.WarningSentenceId).ToList();
        var itemsExists = itemIds.Contains(dto.WarningSentenceId);

        if (!itemsExists) throw new Exception("Warning Sentence not found.");

        //Add warning sentence to product
        var productWarningSentence = new ProductWarningSentence
        {
            ProductId = dto.ProductId,
            WarningSentenceId = dto.WarningSentenceId
        };

        var result = await _productWarningSentenceRepository.AddAsync(productWarningSentence);

        //Sync with SEA database
        await _syncProducer.ProduceAsync(Config.Kafka.Topics.SyncAddProduct,
            new SyncProductWarningSentenceDto
                { ProductId = result.ProductId, WarningSentenceId = result.WarningSentenceId });

        _logger.LogInformation(
            $"Syncing product with SEA database (Warning Sentence added). ProductId: {result.ProductId}, WarningSentenceId: {result.WarningSentenceId}");

        return result;
    }

    public async Task<ProductWarningSentence> RemoveWarningSentenceAsync(RemoveWsDto dto)
    {
        //Remove warning sentence from product
        var productWarningSentence = new ProductWarningSentence
        {
            ProductId = dto.ProductId,
            WarningSentenceId = dto.WarningSentenceId
        };

        try
        {
            await _productWarningSentenceRepository.DeleteAsync(productWarningSentence);

            //Sync with SEA database
            await _syncProducer.ProduceAsync(Config.Kafka.Topics.SyncDeleteProduct,
                new SyncProductWarningSentenceDto
                {
                    ProductId = productWarningSentence.ProductId,
                    WarningSentenceId = productWarningSentence.WarningSentenceId
                });

            _logger.LogInformation(
                $"Syncing product with SEA database (Warning Sentence removed). ProductId: {productWarningSentence.ProductId}, WarningSentenceId: {productWarningSentence.WarningSentenceId}");

            return productWarningSentence;
        }
        catch (Exception e)
        {
            throw new Exception("Warning Sentence not found on product.");
        }
    }

    public async Task<List<int>> GetProductWarningSentencesAsync(int productId)
    {
        var productWarningSentences =
            await _productWarningSentenceRepository.ListAsync(new GetProductWarningSentencesByProductIdSpec(productId));

        var warningSentenceIds = productWarningSentences.Select(warningSentence => warningSentence.WarningSentenceId)
            .ToList();

        return warningSentenceIds;
    }
}