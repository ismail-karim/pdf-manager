using PDF.Manager.Domain.Entities;

namespace PDF.Manager.Domain.Services;

/// <summary>
/// Defines the contract for merging several PDF documents into a single, compressed PDF document.
/// This abstraction is owned by the domain layer; the concrete implementation lives in an outer
/// layer (Infrastructure), keeping the dependency direction pointing towards the core (Onion architecture).
/// </summary>
public interface IPdfMergeService
{
    /// <summary>
    /// Merges the provided PDF documents, in the given order, into a single compressed PDF document.
    /// </summary>
    /// <param name="orderedDocuments">
    /// The PDF documents to merge, already sorted in the exact order chosen by the user.
    /// </param>
    /// <param name="cancellationToken">A token used to cancel the merge operation.</param>
    /// <returns>The binary content of the resulting merged and compressed PDF document.</returns>
    Task<byte[]> MergeAsync(
        IReadOnlyList<PdfDocument> orderedDocuments,
        CancellationToken cancellationToken = default);
}
