using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.RegularExpressions;
using UniversalBase64Decoder.Core.Interfaces;
using UniversalBase64Decoder.Services.Rendering;
namespace UniversalBase64Decoder.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IBase64DecoderService _decoder;
    private readonly ZplRendererService _renderer;
    [BindProperty]
    public string? InputBase64 { get; set; }
    public string? RenderedImageBase64 { get; set; }
    public string? Result { get; private set; }
    public string? FileType { get; private set; }
    public string? MimeType { get; private set; }
    public string? Extension { get; private set; }
    public long Size { get; private set; }
    public string? RawBase64 { get; private set; }
    public string? DownloadFileName { get; private set; }

    public IndexModel(IBase64DecoderService decoder, ZplRendererService renderer)
    {
        _decoder = decoder;
        _renderer = renderer;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(InputBase64))
        {
            Result = "Please enter a base64 string.";
            return;
        }

        var decoded = _decoder.Decode(InputBase64);

        if (!decoded.Success)
        {
            Result = decoded.ErrorMessage;
            return;
        }

        // Set metadata
        FileType = decoded.DetectedFileType;
        Extension = decoded.SuggestedExtension;
        // Render ZPL using Labelary for preview
        if (FileType == "ZPL Label")
        {
            var zplText = Encoding.UTF8.GetString(decoded.Data!);
            var image = await _renderer.RenderAsync(zplText);

            if (image != null)
            {
                RenderedImageBase64 = Convert.ToBase64String(image);
            }
        }
        Size = decoded.Size;
        MimeType = FileType switch
        {
            "PNG Image" => "image/png",
            "JPEG Image" => "image/jpeg",
            "PDF Document" => "application/pdf",
            _ => "application/octet-stream"
        };
        DownloadFileName = $"decoded{Extension}";
        var rawBytes = decoded.Data!;

        // If ZPL, make it human-readable
        if (FileType == "ZPL Label")
        {
            var text = Encoding.UTF8.GetString(rawBytes);
            text = text.Replace("^", "\n^");
            rawBytes = Encoding.UTF8.GetBytes(text);
        }

        RawBase64 = Convert.ToBase64String(rawBytes);

        // Try showing readable text preview
        Result = TryPreview(decoded.Data!);
    }

    private static string TryPreview(byte[] data)
    {
        try
        {
            var text = Encoding.UTF8.GetString(data);
            // ZPL formatting (pretty preview)
            if (text.Contains("^XA") && text.Contains("^XZ"))
            {
                var formatted = text.Replace("^", "\n^");
                // For readability
                formatted = Regex.Replace(
                    formatted,
                    @"\^GFA[^\n]+",
                    "^GFA [image data omitted for preview]"
                );

                return formatted.Length > 1200
                    ? formatted.Substring(0, 1200) + "\n\n[Preview truncated]"
                    : formatted;
            }
            if (text.Take(100).All(c => !char.IsControl(c) || c == '\n' || c == '\r'))
                return text.Length > 500 ? text.Substring(0, 500) + "..." : text;

            return "[Binary content preview not available]";
        }
        catch
        {
            return "[Binary content preview not available]";
        }
    }
}