namespace Chemicals.Core.Exceptions;

public class ProductsNotFoundException : Exception
{
    public ProductsNotFoundException() : base("No products were found.")
    {
    }
}