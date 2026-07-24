namespace PDF.Manager.Domain.Exceptions;

/// <summary>
/// Thrown when the underlying PDF engine fails to merge the provided documents.
/// </summary>
public sealed class PdfMergeException : PdfManagerDomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfMergeException"/> class.
    /// </summary>
    /// <param name="message">A human readable description of the failure.</param>
    /// <param name="innerException">The exception raised by the underlying PDF engine.</param>
    public PdfMergeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
