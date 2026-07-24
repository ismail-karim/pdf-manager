namespace PDF.Manager.Domain.Exceptions;

/// <summary>
/// Base exception for every business-rule violation raised by the PDF.Manager domain layer.
/// </summary>
public abstract class PdfManagerDomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfManagerDomainException"/> class.
    /// </summary>
    /// <param name="message">A human readable description of the violated business rule.</param>
    protected PdfManagerDomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PdfManagerDomainException"/> class.
    /// </summary>
    /// <param name="message">A human readable description of the violated business rule.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected PdfManagerDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
