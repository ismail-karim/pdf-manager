using PDF.Manager.Application.Dtos;
using PDF.Manager.Application.Tests.Doubles;
using PDF.Manager.Application.UseCases;
using PDF.Manager.Domain.Exceptions;
using Xunit;

namespace PDF.Manager.Application.Tests.UseCases;

/// <summary>
/// Unit tests for <see cref="MergePdfDocumentsUseCase"/>.
/// </summary>
public sealed class MergePdfDocumentsUseCaseTests
{
    private static readonly byte[] FakeContent = { 0x25, 0x50, 0x44, 0x46 }; // "%PDF"

    [Fact]
    public async Task ExecuteAsync_WithFewerThanTwoFiles_ThrowsInsufficientPdfDocumentsException()
    {
        var mergeService = new FakePdfMergeService(FakeContent);
        var useCase = new MergePdfDocumentsUseCase(mergeService);
        var request = new MergePdfDocumentsRequest
        {
            Files = new[] { new PdfFileToMerge("only-one.pdf", FakeContent) },
        };

        await Assert.ThrowsAsync<InsufficientPdfDocumentsException>(() => useCase.ExecuteAsync(request));
        Assert.Equal(0, mergeService.InvocationCount);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidFiles_PreservesTheUserDefinedOrder()
    {
        var mergeService = new FakePdfMergeService(FakeContent);
        var useCase = new MergePdfDocumentsUseCase(mergeService);
        var request = new MergePdfDocumentsRequest
        {
            Files = new[]
            {
                new PdfFileToMerge("second.pdf", FakeContent),
                new PdfFileToMerge("first.pdf", FakeContent),
            },
        };

        await useCase.ExecuteAsync(request);

        Assert.Equal(1, mergeService.InvocationCount);
        Assert.NotNull(mergeService.ReceivedDocuments);
        Assert.Equal("second.pdf", mergeService.ReceivedDocuments![0].FileName);
        Assert.Equal("first.pdf", mergeService.ReceivedDocuments![1].FileName);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidFiles_ReturnsMergedContentAndPdfFileName()
    {
        var mergeService = new FakePdfMergeService(FakeContent);
        var useCase = new MergePdfDocumentsUseCase(mergeService);
        var request = new MergePdfDocumentsRequest
        {
            Files = new[]
            {
                new PdfFileToMerge("a.pdf", FakeContent),
                new PdfFileToMerge("b.pdf", FakeContent),
            },
        };

        MergePdfDocumentsResult result = await useCase.ExecuteAsync(request);

        Assert.Equal(FakeContent, result.Content);
        Assert.EndsWith(".pdf", result.FileName);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonPdfFile_ThrowsInvalidPdfDocumentException()
    {
        var mergeService = new FakePdfMergeService(FakeContent);
        var useCase = new MergePdfDocumentsUseCase(mergeService);
        var request = new MergePdfDocumentsRequest
        {
            Files = new[]
            {
                new PdfFileToMerge("document.txt", FakeContent),
                new PdfFileToMerge("valid.pdf", FakeContent),
            },
        };

        await Assert.ThrowsAsync<InvalidPdfDocumentException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public void Constructor_WithNullMergeService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new MergePdfDocumentsUseCase(null!));
    }
}
