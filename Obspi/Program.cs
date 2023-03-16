using Microsoft.Extensions.Options;
using Obspi;
using Obspi.Config;
using Obspi.Devices;
using Obspi.Devices.I2c;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<I2cDevicesOptions>(options =>
    builder.Configuration.GetSection(I2cDevicesOptions.I2cDevices).Bind(options));
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
builder.Services.AddSingleton<Observatory>();

builder.Services.AddHostedService<ObservatoryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();