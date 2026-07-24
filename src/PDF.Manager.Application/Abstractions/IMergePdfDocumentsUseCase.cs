using PDF.Manager.Application.Dtos;

namespace PDF.Manager.Application.Abstractions;

/// <summary>
/// Defines the application use case that merges several PDF documents into a single compressed document.
/// This is the entry point used by the presentation layer.
/// </summary>
public interface IMergePdfDocumentsUseCase
{
    /// <summary>
    /// Executes the merge use case.
    /// </summary>
    /// <param name="request">The ordered collection of PDF files to merge.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The merged and compressed PDF document.</returns>
    Task<MergePdfDocumentsResult> ExecuteAsync(
        MergePdfDocumentsRequest request,
        CancellationToken cancellationToken = default);
}
