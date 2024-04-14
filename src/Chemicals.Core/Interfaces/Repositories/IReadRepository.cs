using Ardalis.Specification;

namespace Chemicals.Core.Interfaces.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}