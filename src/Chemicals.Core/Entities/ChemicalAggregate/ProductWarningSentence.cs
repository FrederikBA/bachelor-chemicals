using Shared.Integration.Interfaces;

namespace Chemicals.Core.Entities.ChemicalAggregate;

public class ProductWarningSentence : IAggregateRoot //Link table
{
    public int ProductId { get; set; }
    public int WarningSentenceId { get; set; }
}