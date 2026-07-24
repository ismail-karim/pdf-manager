using PDF.Manager.Domain.Exceptions;

namespace PDF.Manager.Domain.Entities;

/// <summary>
/// Represents a single PDF document supplied by the user that will take part in a merge operation.
/// The constructor guarantees that only valid documents can ever exist inside the domain.
/// </summary>
public sealed class PdfDocument
{
    private const string PdfExtension = ".pdf";

    /// <summary>
    /// Initializes a new instance of the <see cref="PdfDocument"/> class.
    /// </summary>
    /// <param name="fileName">The original file name of the document, including its extension.</param>
    /// <param name="content">The raw binary content of the PDF document.</param>
    /// <exception cref="InvalidPdfDocumentException">Thrown when the file name or the content is invalid.</exception>
    public PdfDocument(string fileName, byte[] content)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new InvalidPdfDocumentException("The PDF file name must be provided.");
        }

        if (!fileName.EndsWith(PdfExtension, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidPdfDocumentException($"The file '{fileName}' is not a PDF document.");
        }

        if (content is null || content.Length == 0)
        {
            throw new InvalidPdfDocumentException($"The PDF document '{fileName}' has no content.");
        }

        FileName = fileName;
        Content = content;
    }

    /// <summary>Gets the original file name of the PDF document, including its extension.</summary>
    public string FileName { get; }

    /// <summary>Gets the raw binary content of the PDF document.</summary>
    public byte[] Content { get; }
}
