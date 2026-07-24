using PDF.Manager.Application.Abstractions;
using PDF.Manager.Application.Dtos;
using PDF.Manager.Domain.Entities;
using PDF.Manager.Domain.Exceptions;
using PDF.Manager.Domain.Services;

namespace PDF.Manager.Application.UseCases;

/// <summary>
/// Default implementation of <see cref="IMergePdfDocumentsUseCase"/>.
/// It validates the request, maps the incoming DTOs to validated domain entities while preserving
/// the user-defined order, delegates the actual merge to the domain <see cref="IPdfMergeService"/>,
/// and builds the result returned to the presentation layer.
/// </summary>
public sealed class MergePdfDocumentsUseCase : IMergePdfDocumentsUseCase
{
    /// <summary>The minimum number of documents required to perform a merge.</summary>
    public const int MinimumNumberOfDocumentsToMerge = 2;

    private readonly IPdfMergeService _pdfMergeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MergePdfDocumentsUseCase"/> class.
    /// </summary>
    /// <param name="pdfMergeService">The domain service that performs the actual PDF merge.</param>
    public MergePdfDocumentsUseCase(IPdfMergeService pdfMergeService)
    {
        _pdfMergeService = pdfMergeService ?? throw new ArgumentNullException(nameof(pdfMergeService));
    }

    /// <inheritdoc />
    public async Task<MergePdfDocumentsResult> ExecuteAsync(
        MergePdfDocumentsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        int numberOfFiles = request.Files?.Count ?? 0;
        if (numberOfFiles < MinimumNumberOfDocumentsToMerge)
        {
            throw new InsufficientPdfDocumentsException(MinimumNumberOfDocumentsToMerge, numberOfFiles);
        }

        IReadOnlyList<PdfDocument> orderedDocuments = request.Files!
            .Select(file => new PdfDocument(file.FileName, file.Content))
            .ToList();

        byte[] mergedContent = await _pdfMergeService
            .MergeAsync(orderedDocuments, cancellationToken)
            .ConfigureAwait(false);

        return new MergePdfDocumentsResult(BuildOutputFileName(), mergedContent);
    }

    /// <summary>
    /// Builds a unique, human readable file name for the merged document based on the current UTC time.
    /// </summary>
    /// <returns>The suggested output file name.</returns>
    private static string BuildOutputFileName()
    {
        return $"merged-{DateTime.UtcNow:yyyyMMdd-HHmmss}.pdf";
    }
}
