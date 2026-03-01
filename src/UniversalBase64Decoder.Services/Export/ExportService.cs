using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace UniversalBase64Decoder.Services.Export;

public static class ExportService
{
    public static byte[] GeneratePdfFromImage(byte[] imageBytes)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                page.Content()
                    .AlignCenter()
                    .Image(imageBytes, ImageScaling.FitArea);
            });
        }).GeneratePdf();
    }
}