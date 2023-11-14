using Microsoft.Extensions.Options;
using Obspi.Devices;
using Obspi;
using Obspi.Config;
using Obspi.Devices.I2c;
using Obspi.Services;
using Microsoft.AspNetCore.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<I2cDevicesOptions>(options =>
    builder.Configuration.GetSection(I2cDevicesOptions.I2cDevices).Bind(options));
builder.Services.Configure<PushoverOptions>(options =>
    builder.Configuration.GetSection(PushoverOptions.Pushover).Bind(options));
builder.Services.Configure<SqmOptions>(builder.Configuration.GetSection("Sqm"));
builder.Services.AddSingleton<I2cLock>();
builder.Services.AddTransient<Func<int, int, II2cDevice>>(provider =>
{
    var i2cLock = provider.GetRequiredService<I2cLock>();
    var options = provider.GetRequiredService<IOptions<I2cDevicesOptions>>();
    if (options.Value.UseInMemoryI2c)
        return (bus, addr) => new InMemoryI2cDevice(bus, addr, i2cLock);
    return (bus, addr) => new ThreadSafeI2cDevice(bus, addr, i2cLock);
});
builder.Services.AddSingleton<ObspiIO>(provider =>
{
    var options = provider.GetRequiredService<IOptions<I2cDevicesOptions>>();
    var i2cFunc = provider.GetRequiredService<Func<int, int, II2cDevice>>();
    var inputs = new List<InputBank16>(options.Value.InputBanks
        .Select(addr => new InputBank16(i2cFunc(1, addr))));
    var outputs = new List<OutputBank16>(options.Value.OutputBanks
        .Select(addr => new OutputBank16(i2cFunc(1, addr))));
    return new(inputs, outputs);
});
builder.Services.AddSingleton<IndustrialAutomation>(provider =>
{
    var options = provider.GetRequiredService<IOptions<I2cDevicesOptions>>();
    var i2cFunc = provider.GetRequiredService<Func<int, int, II2cDevice>>();
    return new(i2cFunc(1, options.Value.Watchdog));
});
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<SqmLe>();
builder.Services.AddSingleton<CloudWatcher>();
builder.Services.AddSingleton<Observatory>();
builder.Services.AddTransient<INotificationService, PushoverService>();

builder.Services.AddHostedService<ObservatoryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", config => config
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddSpaStaticFiles(config =>
{
    config.RootPath = "wwwroot";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapWhen(x => !x.Request.Path.StartsWithSegments("/api"), client =>
    {
        client.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
        });
    });
}
else
{
    app.MapWhen(x => !x.Request.Path.StartsWithSegments("/api"), client =>
    {
        client.UseSpaStaticFiles();
        client.UseSpa(spa =>
        {
            spa.Options.SourcePath = "wwwroot";

            // adds no-store header to index page to prevent deployment issues (prevent linking to old .js files)
            // .js and other static resources are still cached by the browser
            spa.Options.DefaultPageStaticFileOptions = new()
            {
                OnPrepareResponse = ctx =>
                {
                    ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new()
                    {
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true,
                    };
                }
            };
        });
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();