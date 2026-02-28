using System.Text.RegularExpressions;
using UniversalBase64Decoder.Core.Interfaces;
using UniversalBase64Decoder.Core.Models;
using UniversalBase64Decoder.Services.Detection;

namespace UniversalBase64Decoder.Services.Decoding;

public class Base64DecoderService : IBase64DecoderService
{
    private const int MaxInputSizeBytes = 5 * 1024 * 1024;

    public DecodeResult Decode(string base64Input)
    {
        if (string.IsNullOrWhiteSpace(base64Input))
            return Fail("Input is empty.");

        try
        {
            var sanitized = Sanitize(base64Input);

            if (sanitized.Length > MaxInputSizeBytes * 1.37)
                return Fail("Input too large.");

            var bytes = Convert.FromBase64String(sanitized);

            var (type, ext) = FileTypeDetector.Detect(bytes);

            return new DecodeResult
            {
                Success = true,
                Data = bytes,
                DetectedFileType = type,
                SuggestedExtension = ext
            };
        }
        catch (FormatException)
        {
            return Fail("Invalid Base64 format.");
        }
        catch (Exception ex)
        {
            return Fail($"Unexpected error: {ex.Message}");
        }
    }

    private static string Sanitize(string input)
        => Regex.Replace(input, @"\s+", "");

    private static DecodeResult Fail(string message)
    {
        return new DecodeResult
        {
            Success = false,
            ErrorMessage = message
        };
    }
}