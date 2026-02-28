namespace UniversalBase64Decoder.Core.Models;

public class DecodeResult
{
    public bool Success { get; set; }

    public byte[]? Data { get; set; }

    public string? ErrorMessage { get; set; }

    public long Size => Data?.LongLength ?? 0;

    // NEW 👇
    public string? DetectedFileType { get; set; }
    public string? SuggestedExtension { get; set; }
}