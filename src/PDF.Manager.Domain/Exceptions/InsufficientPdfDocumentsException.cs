namespace PDF.Manager.Domain.Exceptions;

/// <summary>
/// Thrown when a merge operation is requested with fewer PDF documents than the minimum required.
/// </summary>
public sealed class InsufficientPdfDocumentsException : PdfManagerDomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InsufficientPdfDocumentsException"/> class.
    /// </summary>
    /// <param name="minimumRequired">The minimum number of documents required to perform a merge.</param>
    /// <param name="providedCount">The number of documents that were actually provided.</param>
    public InsufficientPdfDocumentsException(int minimumRequired, int providedCount)
        : base($"At least {minimumRequired} PDF documents are required to perform a merge, but {providedCount} were provided.")
    {
        MinimumRequired = minimumRequired;
        ProvidedCount = providedCount;
    }

    /// <summary>Gets the minimum number of documents required to perform a merge.</summary>
    public int MinimumRequired { get; }

    /// <summary>Gets the number of documents that were actually provided.</summary>
    public int ProvidedCount { get; }
}
