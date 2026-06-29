using System.Text;
using Microsoft.AspNetCore.Http;

namespace SiradigCalc.Tests.Common;

internal sealed class FakeFormFile : IFormFile
{
    private readonly byte[] _content;

    public FakeFormFile(string content, string fileName = "test.csv", string contentType = "text/csv")
    {
        _content = Encoding.UTF8.GetBytes(content);
        FileName = fileName;
        ContentType = contentType;
        Length = _content.Length;
    }

    public string ContentType { get; }
    public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{FileName}\"";
    public IHeaderDictionary Headers => new HeaderDictionary();
    public long Length { get; }
    public string Name => "file";
    public string FileName { get; }

    public void CopyTo(Stream target) => target.Write(_content);
    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        => target.WriteAsync(_content, cancellationToken).AsTask();
    public Stream OpenReadStream() => new MemoryStream(_content);
}
