using System.Text;

namespace UniversalBase64Decoder.Services.Detection;

public static class FileTypeDetector
{
    public static (string type, string extension) Detect(byte[] data)
    {
        if (data.Length < 4)
            return ("Unknown", ".bin");

        // PDF: %PDF
        if (StartsWith(data, "%PDF"u8.ToArray()))
            return ("PDF Document", ".pdf");

        // PNG: 89 50 4E 47
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return ("PNG Image", ".png");

        // JPG: FF D8
        if (data[0] == 0xFF && data[1] == 0xD8)
            return ("JPEG Image", ".jpg");

        // ZPL heuristic (text-based)
        var textSample = Encoding.UTF8.GetString(data.Take(100).ToArray());
        if (textSample.Contains("^XA"))
            return ("ZPL Label", ".zpl");

        return ("Unknown Binary", ".bin");
    }

    private static bool StartsWith(byte[] data, byte[] signature)
    {
        if (data.Length < signature.Length)
            return false;

        for (int i = 0; i < signature.Length; i++)
        {
            if (data[i] != signature[i])
                return false;
        }

        return true;
    }
}