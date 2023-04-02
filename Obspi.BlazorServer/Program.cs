using Flurl.Http.Configuration;
using MudBlazor.Services;
using Obspi.BlazorServer.Options;
using Obspi.BlazorServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ObspiOptions>(builder.Configuration.GetSection(ObspiOptions.Obspi));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
builder.Services.AddTransient<IObspiService, ObspiService>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 150;
    config.SnackbarConfiguration.ShowTransitionDuration = 150;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();