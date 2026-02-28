using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversalBase64Decoder.Core.Interfaces;
using System.Text;

namespace UniversalBase64Decoder.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IBase64DecoderService _decoder;

    [BindProperty]
    public string? InputBase64 { get; set; }

    public string? Result { get; private set; }

    public string? FileType { get; private set; }
    public string? MimeType { get; private set; }
    public string? Extension { get; private set; }
    public long Size { get; private set; }
    public string? RawBase64 { get; private set; }
    public string? DownloadFileName { get; private set; }

    public IndexModel(IBase64DecoderService decoder)
    {
        _decoder = decoder;
    }

    public void OnGet()
    {
    }

    public void OnPost()
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
        Size = decoded.Size;
        MimeType = FileType switch
        {
            "PNG Image" => "image/png",
            "JPEG Image" => "image/jpeg",
            "PDF Document" => "application/pdf",
            _ => "application/octet-stream"
        };
        DownloadFileName = $"decoded{Extension}";
        RawBase64 = Convert.ToBase64String(decoded.Data!);

        // Try showing readable text preview
        Result = TryPreview(decoded.Data!);
    }

    private static string TryPreview(byte[] data)
    {
        try
        {
            var text = Encoding.UTF8.GetString(data);

            // If mostly readable, show text
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