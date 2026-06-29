using FluentValidation;
using NSubstitute;
using SiradigCalc.Application.Commands;
using SiradigCalc.Application.Helpers.Pdf;
using SiradigCalc.Core.Entities;
using SiradigCalc.Tests.Common;

namespace SiradigCalc.Tests.Commands;

public class ImportRecordFromPdfCommandTests
{
    private readonly Guid _templateId = Guid.NewGuid();
    private readonly Guid _fieldId1 = Guid.NewGuid();
    private readonly Guid _fieldId2 = Guid.NewGuid();
    private readonly Guid _fieldId3 = Guid.NewGuid();

    private TestDbContext CreateDb()
    {
        var db = TestDbContext.Create($"pdf-{Guid.NewGuid()}");

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
                        new RecordTemplateField { Id = _fieldId1, Label = "Basico" },
                        new RecordTemplateField { Id = _fieldId2, Label = "Antiguedad" },
                        new RecordTemplateField { Id = _fieldId3, Label = "Horas Extra" }
                    ]
                }
            ]
        });

        db.SaveChanges();
        return db;
    }

    private ImportRecordFromPdfCommandHandler CreateHandler(TestDbContext db, IReceiptPdfTextExtractor extractor)
        => new(db, extractor);

    private ImportRecordFromPdfCommand BuildCommand() => new()
    {
        File = new FakeFormFile("ignored", "test.pdf", "application/pdf"),
        TemplateId = _templateId
    };

    [Fact]
    public async Task Handle_MatchesSingleWordLabelOnLine()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns(["Basico 45000.00"]);

        var result = await CreateHandler(db, extractor).Handle(BuildCommand(), CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId1, result.Values[0].FieldId);
        Assert.Equal("45000.00", result.Values[0].Value);
    }

    [Fact]
    public async Task Handle_MatchesMultiWordLabel()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns(["Horas Extra 1200.50"]);

        var result = await CreateHandler(db, extractor).Handle(BuildCommand(), CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId3, result.Values[0].FieldId);
        Assert.Equal("1200.50", result.Values[0].Value);
    }

    [Fact]
    public async Task Handle_MatchesMultiplePairsOnSameLine()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns(["Basico 45000.00 Antiguedad 5400.00"]);

        var result = await CreateHandler(db, extractor).Handle(BuildCommand(), CancellationToken.None);

        Assert.Equal(2, result.Values.Count);
        Assert.Contains(result.Values, v => v.FieldId == _fieldId1 && v.Value == "45000.00");
        Assert.Contains(result.Values, v => v.FieldId == _fieldId2 && v.Value == "5400.00");
    }

    [Fact]
    public async Task Handle_IsCaseInsensitive()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns(["BASICO 45000.00"]);

        var result = await CreateHandler(db, extractor).Handle(BuildCommand(), CancellationToken.None);

        Assert.Single(result.Values);
        Assert.Equal(_fieldId1, result.Values[0].FieldId);
    }

    [Fact]
    public async Task Handle_OmitsFieldsNotFoundInLines()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns(["Basico 45000.00"]);

        var result = await CreateHandler(db, extractor).Handle(BuildCommand(), CancellationToken.None);

        Assert.DoesNotContain(result.Values, v => v.FieldId == _fieldId2);
        Assert.DoesNotContain(result.Values, v => v.FieldId == _fieldId3);
    }

    [Fact]
    public async Task Handle_ThrowsValidationException_WhenTemplateNotFound()
    {
        var db = CreateDb();
        var extractor = Substitute.For<IReceiptPdfTextExtractor>();
        extractor.ExtractLines(Arg.Any<Stream>()).Returns([]);

        var command = new ImportRecordFromPdfCommand
        {
            File = new FakeFormFile("ignored", "test.pdf", "application/pdf"),
            TemplateId = Guid.NewGuid()
        };

        await Assert.ThrowsAsync<ValidationException>(() =>
            CreateHandler(db, extractor).Handle(command, CancellationToken.None));
    }
}
