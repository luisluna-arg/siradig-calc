using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Configurations;

namespace SiradigCalc.Infra.Persistence.DbContexts;

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