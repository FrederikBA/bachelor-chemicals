using Ardalis.Specification;
using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Exceptions;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Core.Services;
using Chemicals.Test.Helpers;
using Moq;

namespace Chemicals.Test.UnitTests;

public class ProductUnitTests
{
    private readonly IProductService _productService;
    private readonly Mock<IReadRepository<Product>> _productReadRepositoryMock = new();
    
    public ProductUnitTests()
    {
        _productService = new ProductService(_productReadRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsListOfProducts()
    {
        //Arrange
        var testProducts = ProductTestHelper.GetTestProducts();

        _productReadRepositoryMock.Setup(x =>
                x.ListAsync(It.IsAny<ISpecification<Product>>(), new CancellationToken()))
            .ReturnsAsync(testProducts);

        //Act
        var result = await _productService.GetAllProductsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); //Expecting 2 products
    }
    
    [Fact]
    public async Task GetProductsAsync_ThrowsProductsNotFoundException()
    {
        //Arrange
        _productReadRepositoryMock.Setup(x =>
                x.ListAsync(It.IsAny<ISpecification<Product>>(), new CancellationToken()))
            .ReturnsAsync(new List<Product>());

        //Act
        var exception = await Assert.ThrowsAsync<ProductsNotFoundException>(() => _productService.GetAllProductsAsync());

        //Assert
        Assert.NotNull(exception);
    }
}