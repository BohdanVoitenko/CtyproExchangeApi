using System.Reflection;
using System.Text;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<CryptoExchangeDbContext>()
    .AddDefaultTokenProviders();

// 3. Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// 4. Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Issuer"],
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

//builder.Services.ConfigureApplicationCookie(o =>
//{
//    o.Events = new CookieAuthenticationEvents()
//    {
//        OnRedirectToLogin = (ctx) =>
//        {
//            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
//            {
//                ctx.Response.StatusCode = 401;
//            }

//            return Task.CompletedTask;
//        },
//        OnRedirectToAccessDenied = (ctx) =>
//        {
//            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
//            {
//                ctx.Response.StatusCode = 403;
//            }

//            return Task.CompletedTask;
//        }
//    };
//});

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

builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);

    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Crypto Exchange API",
        Description = "ASP.NET Core 3.1 Web API"
    });
    // To Enable authorization using Swagger (JWT)  
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});


builder.Services.AddScoped<ICryptoExchangeDbContext, CryptoExchangeDbContext>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddHttpContextAccessor();



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


app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Exchange API");
});
//PrepDb.PrepPopulation(app);
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

