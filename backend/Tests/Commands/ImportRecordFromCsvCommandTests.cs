using FluentValidation;
using SiradigCalc.Application.Commands;
using SiradigCalc.Core.Entities;
using SiradigCalc.Tests.Common;

namespace SiradigCalc.Tests.Commands;

public class ImportRecordFromCsvCommandTests
{
    private readonly Guid _templateId = Guid.NewGuid();
    private readonly Guid _fieldId1 = Guid.NewGuid();
    private readonly Guid _fieldId2 = Guid.NewGuid();

    private TestDbContext CreateDb()
    {
        var db = TestDbContext.Create($"csv-{Guid.NewGuid()}");

        db.RecordTemplates.Add(new RecordTemplate
        {
            Id = _templateId,
            Name = "Test Template",
            Sections =
            [
                new RecordTemplateSection
                {
                    Name = "Earnings",
                    Fields =
                    [
                        new RecordTemplateField { Id = _fieldId1, Label = "Basic Salary" },
                        new RecordTemplateField { Id = _fieldId2, Label = "Overtime" }
                    ]
                }
            ]
        });

        db.SaveChanges();
        return db;
    }

    private ImportRecordFromCsvCommandHandler CreateHandler(TestDbContext db)
        => new(db);

    [Fact]
    public async Task Handle_ReturnsMatchedValues()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\nEarnings;Basic Salary;5000.00\nEarnings;Overtime;250.50";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = _templateId
        };

        var result = await CreateHandler(db).Handle(command, CancellationToken.None);

        Assert.Equal(_templateId, result.TemplateId);
        Assert.Equal(2, result.Values.Count);
        Assert.Contains(result.Values, v => v.FieldId == _fieldId1 && v.Value == "5000.00");
        Assert.Contains(result.Values, v => v.FieldId == _fieldId2 && v.Value == "250.50");
    }

    [Fact]
    public async Task Handle_OmitsFieldsNotFoundInCsv()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\nEarnings;Basic Salary;5000.00";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = _templateId
        };

        var result = await CreateHandler(db).Handle(command, CancellationToken.None);

        Assert.Single(result.Values);
        Assert.DoesNotContain(result.Values, v => v.FieldId == _fieldId2);
    }

    [Fact]
    public async Task Handle_StripsQuotesFromFields()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\n\"Earnings\";\"Basic Salary\";3000.00";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = _templateId
        };

        var result = await CreateHandler(db).Handle(command, CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId1, result.Values[0].FieldId);
    }

    [Fact]
    public async Task Handle_SkipsLinesWithNonNumericValue()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\nEarnings;Basic Salary;N/A\nEarnings;Overtime;100.00";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = _templateId
        };

        var result = await CreateHandler(db).Handle(command, CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId2, result.Values[0].FieldId);
    }

    [Fact]
    public async Task Handle_IsCaseInsensitiveForSectionAndLabel()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\nEARNINGS;BASIC SALARY;1500.00";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = _templateId
        };

        var result = await CreateHandler(db).Handle(command, CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId1, result.Values[0].FieldId);
    }

    [Fact]
    public async Task Handle_ThrowsValidationException_WhenTemplateNotFound()
    {
        var db = CreateDb();
        var csv = "Section;Field;Value\nEarnings;Basic Salary;1000.00";
        var command = new ImportRecordFromCsvCommand
        {
            File = new FakeFormFile(csv),
            TemplateId = Guid.NewGuid()
        };

        await Assert.ThrowsAsync<ValidationException>(() =>
            CreateHandler(db).Handle(command, CancellationToken.None));
    }
}
