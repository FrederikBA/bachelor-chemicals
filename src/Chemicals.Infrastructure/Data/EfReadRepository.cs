using Ardalis.Specification.EntityFrameworkCore;
using Chemicals.Core.Interfaces.Repositories;

namespace Chemicals.Infrastructure.Data;

public class EfReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public readonly ChemicalContext ChemicalContext;
    
    public EfReadRepository(ChemicalContext dbContext) : base(dbContext)
    {
        ChemicalContext = dbContext;
    }
}