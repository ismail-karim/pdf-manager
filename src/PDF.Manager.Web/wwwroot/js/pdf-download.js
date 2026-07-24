// JavaScript interop helpers used by the Blazor Server application to download generated files.
window.pdfManager = {
    // Downloads a file streamed from .NET as a Blob and triggers a browser download.
    downloadFileFromStream: async (fileName, streamReference) => {
        const arrayBuffer = await streamReference.arrayBuffer();
        const blob = new Blob([arrayBuffer], { type: 'application/pdf' });
        const url = URL.createObjectURL(blob);

        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = fileName ?? 'merged.pdf';
        document.body.appendChild(anchor);
        anchor.click();
        anchor.remove();

        URL.revokeObjectURL(url);
    }
};
