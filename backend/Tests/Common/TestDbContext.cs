using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Configurations;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Tests.Common;

internal class TestDbContext : DbContext, ISolutionDbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    public DbSet<ChangeLog> ChangeLogs { get; set; } = default!;
    public DbSet<FieldTypeMapping> FieldTypeMappings { get; set; } = default!;
    public DbSet<RecordTemplate> RecordTemplates { get; set; } = default!;
    public DbSet<RecordTemplateSection> RecordTemplateSections { get; set; } = default!;
    public DbSet<RecordTemplateField> RecordTemplateFields { get; set; } = default!;
    public DbSet<RecordValue> RecordValues { get; set; } = default!;
    public DbSet<RecordTemplateLink> RecordTemplateLinks { get; set; } = default!;
    public DbSet<RecordConversion> RecordConversions { get; set; } = default!;
    public DbSet<SiradigCalc.Core.Entities.Record> Records { get; set; } = default!;
    public DbSet<RecordTemplateFieldLink> RecordFieldLinks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecordConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    internal static TestDbContext Create(string name)
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(name)
            .Options;
        return new TestDbContext(options);
    }
}
