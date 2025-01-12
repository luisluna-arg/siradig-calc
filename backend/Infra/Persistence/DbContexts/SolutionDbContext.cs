using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Configurations.Forms;

namespace SiradigCalc.Infra.Persistence.DbContexts;

public partial class SolutionDbContext : DbContext, ISolutionDbContext
{
    public DbSet<ChangeLog> ChangeLogs { get; set; } = default!;
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; } = default!;
    public DbSet<Form> Forms { get; set; } = default!;
    public DbSet<FormField> FormFields { get; set; } = default!;
    public DbSet<FormTemplate> FormTemplates { get; set; } = default!;
    public DbSet<FormValue> FormValues { get; set; } = default!;
    public DbSet<Receipt> Receipts { get; set; } = default!;
    public DbSet<ReceiptField> ReceiptFields { get; set; } = default!;
    public DbSet<ReceiptTemplate> ReceiptTemplates { get; set; } = default!;
    public DbSet<ReceiptValue> ReceiptValues { get; set; } = default!;
    public DbSet<Record> Records { get; set; } = default!;
    public DbSet<ReceiptToFormConversion> ReceiptToFormConversions { get; set; } = default!;
    public DbSet<RecordFieldLink> RecordFieldLinks { get; set; } = default!;
    public DbSet<RecordTemplate> RecordTemplates { get; set; } = default!;
    public DbSet<RecordTemplateLink> RecordTemplateLinks { get; set; } = default!;

    public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}