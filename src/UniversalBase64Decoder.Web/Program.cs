using UniversalBase64Decoder.Core.Interfaces;
using UniversalBase64Decoder.Services.Decoding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IBase64DecoderService, Base64DecoderService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();