using QuestPDF.Infrastructure;
using UniversalBase64Decoder.Core.Interfaces;
using UniversalBase64Decoder.Services.Decoding;
using UniversalBase64Decoder.Services.Rendering;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IBase64DecoderService, Base64DecoderService>();
builder.Services.AddHttpClient<ZplRendererService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();