using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SiradigCalc.Core.Entities;

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
    public DbSet<RecordTemplateConversion> RecordTemplateConversions { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<RecordTemplateFieldLink> RecordFieldLinks { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;

    DatabaseFacade Database { get; }
}