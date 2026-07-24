namespace PDF.Manager.Application.Dtos;

/// <summary>
/// Represents the input of a merge operation: the ordered collection of PDF files to combine.
/// The order of <see cref="Files"/> is the exact order chosen by the user and defines the
/// order of the pages in the resulting document.
/// </summary>
public sealed record MergePdfDocumentsRequest
{
    /// <summary>Gets the PDF files to merge, already sorted in the desired output order.</summary>
    public IReadOnlyList<PdfFileToMerge> Files { get; init; } = Array.Empty<PdfFileToMerge>();
}
