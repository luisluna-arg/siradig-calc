using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Configurations;

namespace SiradigCalc.Infra.Persistence.DbContexts;

public interface ISolutionDbContext
{
    public DbSet<ChangeLog> ChangeLogs { get; set; }
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; }
    public DbSet<RecordTemplate> RecordTemplates { get; set; }
    public DbSet<RecordTemplateSection> RecordTemplateSections { get; set; }
    public DbSet<RecordTemplateField> RecordTemplateFields { get; set; }
    public DbSet<RecordValue> RecordValues { get; set; }
    public DbSet<RecordTemplateLink> RecordTemplateLinks { get; set; }
    public DbSet<RecordConversion> RecordConversions { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<RecordTemplateFieldLink> RecordFieldLinks { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;

    DatabaseFacade Database { get; }
}

public partial class SolutionDbContext : DbContext, ISolutionDbContext
{
    public DbSet<ChangeLog> ChangeLogs { get; set; } = default!;
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; } = default!;
    public DbSet<RecordTemplate> RecordTemplates { get; set; } = default!;
    public DbSet<RecordTemplateSection> RecordTemplateSections { get; set; } = default!;
    public DbSet<RecordTemplateField> RecordTemplateFields { get; set; } = default!;
    public DbSet<RecordValue> RecordValues { get; set; } = default!;
    public DbSet<RecordTemplateLink> RecordTemplateLinks { get; set; } = default!;
    public DbSet<RecordConversion> RecordConversions { get; set; } = default!;
    public DbSet<Record> Records { get; set; } = default!;
    public DbSet<RecordTemplateFieldLink> RecordFieldLinks { get; set; } = default!;

    public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecordConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}