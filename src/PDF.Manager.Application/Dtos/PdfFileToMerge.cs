namespace PDF.Manager.Application.Dtos;

/// <summary>
/// Represents a single PDF file provided by the presentation layer to be merged.
/// This DTO decouples the outer layers from the domain entities.
/// </summary>
/// <param name="FileName">The original file name of the PDF file, including its extension.</param>
/// <param name="Content">The raw binary content of the PDF file.</param>
public sealed record PdfFileToMerge(string FileName, byte[] Content);
