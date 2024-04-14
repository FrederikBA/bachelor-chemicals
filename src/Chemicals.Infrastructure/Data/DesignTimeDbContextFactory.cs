using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Chemicals.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChemicalContext>
{
    public ChemicalContext CreateDbContext(string[] args)
    {
        //TODO: Move connection string to appsettings.json / configuration file
        const string connectionString = "Server=localhost;Database=ShwChemicals;User Id=sa;Password=thisIsSuperStrong1234;TrustServerCertificate=True";
        
        var optionsBuilder = new DbContextOptionsBuilder<ChemicalContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ChemicalContext(optionsBuilder.Options);
    }
}