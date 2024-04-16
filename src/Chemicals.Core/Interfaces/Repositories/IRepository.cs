using Ardalis.Specification;

namespace Chemicals.Core.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}