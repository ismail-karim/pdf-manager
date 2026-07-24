namespace PDF.Manager.Domain.Exceptions;

/// <summary>
/// Thrown when a PDF document does not satisfy the invariants required to take part in a merge operation.
/// </summary>
public sealed class InvalidPdfDocumentException : PdfManagerDomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPdfDocumentException"/> class.
    /// </summary>
    /// <param name="message">A human readable description of the invalid document.</param>
    public InvalidPdfDocumentException(string message)
        : base(message)
    {
    }
}
