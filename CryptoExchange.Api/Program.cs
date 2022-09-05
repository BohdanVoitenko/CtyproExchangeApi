using System.Reflection;
using CryptoExchange.Application;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Middleware;
using CryptoExchange.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ICryptoExchangeDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<ICryptoExchangeDbContext, CryptoExchangeDbContext>();



try
{
    // I build a new service provider from the services collection
    using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
    {
        // Review the FormMain Singleton.
        var context = serviceProvider.GetRequiredService<CryptoExchangeDbContext>();
        DbInitializer.Initialize(context);
    }
}
catch(Exception exception)
{
    var ex = exception.Message;
    var exdata = exception.Data;
}



var app = builder.Build();


//try
//{
//    var context = app.Services.GetRequiredService<CryptoExchangeDbContext>();
//    var test = context;
//    DbInitializer.Initialize(context);
//}
//catch (Exception exception)
//{
//    var ex = exception.Message;
//    var exdata = exception.Data;
//}
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
app.UseEndpoints(options =>
{
    options.MapControllers();
});


//app.MapGet("/", () => "Hello World!");

app.Run();

