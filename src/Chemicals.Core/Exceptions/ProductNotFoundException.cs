namespace Chemicals.Core.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId) : base($"Product with id {productId} was not found.")
    {
    }
}