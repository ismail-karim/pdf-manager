using PDF.Manager.Domain.Entities;
using PDF.Manager.Domain.Services;

namespace PDF.Manager.Application.Tests.Doubles;

/// <summary>
/// A test double for <see cref="IPdfMergeService"/> that records the documents it received
/// and returns a predefined result, avoiding any real dependency on the PDF engine.
/// </summary>
internal sealed class FakePdfMergeService : IPdfMergeService
{
    private readonly byte[] _resultToReturn;

    /// <summary>
    /// Initializes a new instance of the <see cref="FakePdfMergeService"/> class.
    /// </summary>
    /// <param name="resultToReturn">The byte array that the fake should return when invoked.</param>
    public FakePdfMergeService(byte[] resultToReturn)
    {
        _resultToReturn = resultToReturn;
    }

    /// <summary>Gets the documents received during the last call, in the order they were provided.</summary>
    public IReadOnlyList<PdfDocument>? ReceivedDocuments { get; private set; }

    /// <summary>Gets the number of times the service was invoked.</summary>
    public int InvocationCount { get; private set; }

    /// <inheritdoc />
    public Task<byte[]> MergeAsync(
        IReadOnlyList<PdfDocument> orderedDocuments,
        CancellationToken cancellationToken = default)
    {
        InvocationCount++;
        ReceivedDocuments = orderedDocuments;
        return Task.FromResult(_resultToReturn);
    }
}
