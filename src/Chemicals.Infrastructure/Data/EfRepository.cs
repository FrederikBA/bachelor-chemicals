using Ardalis.Specification.EntityFrameworkCore;
using Chemicals.Core.Interfaces;
using Chemicals.Core.Interfaces.Repositories;

namespace Chemicals.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public readonly ChemicalContext ChemicalContext;
    
    public EfRepository(ChemicalContext dbContext) : base(dbContext)
    {
        ChemicalContext = dbContext;
    }
}