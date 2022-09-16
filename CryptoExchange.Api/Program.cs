using System.Reflection;
using AutoMapper;
using CryptoExchange.Application;
using CryptoExchange.Application.Common.Caching;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Middleware;
using CryptoExchange.Application.UsersAuth.Commands;
using CryptoExchange.Domain;
using CryptoExchange.Persistence;
using CryptoExchange.Persistence.EntityTypeConfigurations;
using MediatR;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<CryptoExchangeDbContext>();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ICryptoExchangeDbContext).Assembly));
});
builder.Services.AddApplication();

var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance)
.Cast<IInstaller>().ToList();

installers.ForEach(installer => installer.InstallServices(builder.Services, builder.Configuration));

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
    using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
    {
        var context = serviceProvider.GetRequiredService<CryptoExchangeDbContext>();
        DbInitializer.Initialize(context);
    }
}
catch(Exception exception)
{
    var ex = exception.Message;
    Console.WriteLine(ex);
    var exdata = exception.Data;
}


var app = builder.Build();



app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(options =>
{
    options.MapControllers();
});



app.Run();

