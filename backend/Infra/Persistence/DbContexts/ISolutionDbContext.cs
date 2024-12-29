using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Infra.Persistence.DbContexts;

public interface ISolutionDbContext
{
    public DbSet<Form> Forms { get; set; }
    public DbSet<FormField> FormFields { get; set; }
    public DbSet<FormTemplate> FormTemplates { get; set; }
    public DbSet<FormValue> FormValues { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptField> ReceiptFields { get; set; }
    public DbSet<ReceiptTemplate> ReceiptTemplates { get; set; }
    public DbSet<ReceiptValue> ReceiptValues { get; set; }
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }
    public DbSet<RecordTemplateLink> RecordTemplateLinks { get; set; }
    public DbSet<RecordFieldLink> RecordFieldLinks { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;

    DatabaseFacade Database { get; }
}