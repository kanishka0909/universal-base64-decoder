using UniversalBase64Decoder.Core.Models;

namespace UniversalBase64Decoder.Core.Interfaces;

public interface IBase64DecoderService
{
    DecodeResult Decode(string base64Input);
}