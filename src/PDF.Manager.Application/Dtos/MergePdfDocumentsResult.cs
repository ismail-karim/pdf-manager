namespace PDF.Manager.Application.Dtos;

/// <summary>
/// Represents the output of a merge operation: the single merged and compressed PDF document.
/// </summary>
/// <param name="FileName">The suggested file name for the merged document.</param>
/// <param name="Content">The raw binary content of the merged and compressed PDF document.</param>
public sealed record MergePdfDocumentsResult(string FileName, byte[] Content);
