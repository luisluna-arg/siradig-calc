using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Configurations.Forms;

namespace SiradigCalc.Infra.Persistence.DbContexts;

public partial class SolutionDbContext : DbContext, ISolutionDbContext
{
    public DbSet<Form> Forms { get; set; } = null!;
    public DbSet<FormField> FormFields { get; set; } = null!;
    public DbSet<FormTemplate> FormTemplates { get; set; } = null!;
    public DbSet<FormValue> FormValues { get; set; } = null!;
    public DbSet<Receipt> Receipts { get; set; } = null!;
    public DbSet<ReceiptField> ReceiptFields { get; set; } = null!;
    public DbSet<ReceiptTemplate> ReceiptTemplates { get; set; } = null!;
    public DbSet<ReceiptValue> ReceiptValues { get; set; } = null!;
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; } = null!;
    public DbSet<ChangeLog> ChangeLogs { get; set; } = null!;

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