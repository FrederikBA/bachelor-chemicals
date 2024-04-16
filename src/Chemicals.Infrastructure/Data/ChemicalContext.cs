using Chemicals.Core.Entities.ChemicalAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chemicals.Infrastructure.Data;

public class ChemicalContext : DbContext
{
    //Product Aggregate
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductCategory>? ProductCategories { get; set; }
    public DbSet<ProductGroup>? ProductGroups { get; set; }
    public DbSet<Producer>? Producers { get; set; }
    public DbSet<ProductWarningSentence>? ProductWarningSentences { get; set; }

    public ChemicalContext(DbContextOptions<ChemicalContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Primary keys
        modelBuilder.Entity<Product>().HasKey(product => product.Id);
        modelBuilder.Entity<ProductCategory>().HasKey(productCategory => productCategory.Id);
        modelBuilder.Entity<ProductGroup>().HasKey(productGroup => productGroup.Id);
        modelBuilder.Entity<Producer>().HasKey(producer => producer.Id);
        modelBuilder.Entity<ProductStatus>().HasKey(productStatus => productStatus.Id);
        
        //Composite keys
        modelBuilder.Entity<ProductWarningSentence>().HasKey(productWarningSentence => new {productWarningSentence.ProductId, productWarningSentence.WarningSentenceId});

        //Add Value Objects
        modelBuilder.Entity<ProducerAddress>(ConfigureAddress);

        //Relations

        //Product to ProductCategory (many to one)
        modelBuilder.Entity<Product>()
            .HasOne(product => product.ProductCategory)
            .WithMany(productCategory => productCategory.Products)
            .HasForeignKey(p => p.ProductCategoryId);

        
        //ProductCategory to ProductGroup (many to one)
        modelBuilder.Entity<ProductCategory>()
            .HasOne(productCategory => productCategory.ProductGroup)
            .WithMany(productGroup => productGroup.ProductCategories)
            .HasForeignKey(p => p.ProductGroupId);
        
        //Product to Producer (many to one)
        modelBuilder.Entity<Product>()
            .HasOne(product => product.Producer)
            .WithMany(producer => producer.Products)
            .HasForeignKey(p => p.ProducerId);
        
        //Product to ProductStatus (many to one)
        modelBuilder.Entity<Product>()
            .HasOne(product => product.ProductStatus)
            .WithMany(productStatus => productStatus.Products)
            .HasForeignKey(p => p.ProductStatusId);
        
        //Address value object
        void ConfigureAddress<T>(EntityTypeBuilder<T> entity) where T : ProducerAddress
        {
            entity.ToTable("ProducerAddress", "dbo");

            entity.Property<int>("Id")
                .IsRequired();
            entity.HasKey("Id");
        }
    }
}