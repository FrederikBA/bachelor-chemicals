using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Integration.Configuration;

namespace Chemicals.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChemicalContext>
{
    public ChemicalContext CreateDbContext(string[] args)
    {
        const string connectionString = Config.ConnectionStrings.ShwChemicals;
        
        var optionsBuilder = new DbContextOptionsBuilder<ChemicalContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ChemicalContext(optionsBuilder.Options);
    }
}