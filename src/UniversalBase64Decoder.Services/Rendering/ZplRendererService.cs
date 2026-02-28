using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UniversalBase64Decoder.Services.Rendering;

public class ZplRendererService
{
    private readonly HttpClient _http;

    public ZplRendererService(HttpClient http)
    {
        _http = http;
    }

    public async Task<byte[]?> RenderAsync(string zpl)
    {
        try
        {
            var content = new StringContent(zpl, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _http.PostAsync(
                "http://api.labelary.com/v1/printers/8dpmm/labels/4x6/0/",
                content
            );

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsByteArrayAsync();
        }
        catch
        {
            return null;
        }
    }
}