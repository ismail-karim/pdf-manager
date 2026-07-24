using System.Text;
using PDF.Manager.Domain.Exceptions;
using PDF.Manager.Domain.Services;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using DomainPdfDocument = PDF.Manager.Domain.Entities.PdfDocument;

namespace PDF.Manager.Infrastructure.Pdf;

/// <summary>
/// Implements <see cref="IPdfMergeService"/> using the PDFsharp library.
/// Each source document is opened in import mode, its pages are appended to a single output document
/// in the requested order, and content-stream compression is enabled to keep the result compact.
/// </summary>
public sealed class PdfSharpMergeService : IPdfMergeService
{
    static PdfSharpMergeService()
    {
        // PDFsharp relies on legacy code-page encodings that are not registered by default on .NET.
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    /// <inheritdoc />
    public Task<byte[]> MergeAsync(
        IReadOnlyList<DomainPdfDocument> orderedDocuments,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderedDocuments);

        try
        {
            byte[] mergedContent = MergeDocuments(orderedDocuments, cancellationToken);
            return Task.FromResult(mergedContent);
        }
        catch (Exception exception) when (exception is not OperationCanceledException
                                          and not PdfManagerDomainException)
        {
            throw new PdfMergeException("An error occurred while merging the PDF documents.", exception);
        }
    }

    /// <summary>
    /// Performs the synchronous merge of the ordered documents into a single compressed PDF document.
    /// </summary>
    /// <param name="orderedDocuments">The documents to merge, in the desired output order.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The binary content of the merged document.</returns>
    private static byte[] MergeDocuments(
        IReadOnlyList<DomainPdfDocument> orderedDocuments,
        CancellationToken cancellationToken)
    {
        using var outputDocument = new PdfDocument();
        ConfigureCompression(outputDocument.Options);

        foreach (DomainPdfDocument document in orderedDocuments)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var inputStream = new MemoryStream(document.Content);
            using PdfDocument inputDocument = PdfReader.Open(inputStream, PdfDocumentOpenMode.Import);

            foreach (PdfPage page in inputDocument.Pages)
            {
                outputDocument.AddPage(page);
            }
        }

        using var outputStream = new MemoryStream();
        outputDocument.Save(outputStream, closeStream: false);
        return outputStream.ToArray();
    }

    /// <summary>
    /// Configures the output document options to produce a compact, compressed PDF document.
    /// </summary>
    /// <param name="options">The output document options to configure.</param>
    private static void ConfigureCompression(PdfDocumentOptions options)
    {
        options.NoCompression = false;
        options.CompressContentStreams = true;
        options.FlateEncodeMode = FlateEncodeMode.BestCompression;
    }
}
