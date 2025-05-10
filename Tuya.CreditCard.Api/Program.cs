using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using System.Text.Json;
using Tuya.CreditCard.Api.Common.Constants;
using Tuya.CreditCard.Api.CrossCutting.Configuration;
using Tuya.CreditCard.Api.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDependencies(configuration);
builder.Services.AddAuthSettings(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(PlatformServices.Default.Application.ApplicationVersion, new OpenApiInfo
    {
        Title = $"{PlatformServices.Default.Application.ApplicationName} - Tuya",
        Version = PlatformServices.Default.Application.ApplicationVersion,
        Description = "API for Credit Card project"
    });

    List<string> xmlFiles = Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));

    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "Introduce un token válido",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    BearerFormat = "JWT",
    //    Scheme = "Bearer"
    //});

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type=ReferenceType.SecurityScheme,
    //                Id="Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocumentTitle = $"{PlatformServices.Default.Application.ApplicationName} {PlatformServices.Default.Application.ApplicationVersion}";
    c.SwaggerEndpoint($"../swagger/{PlatformServices.Default.Application.ApplicationVersion}/swagger.json", $"{PlatformServices.Default.Application.ApplicationName} - Dexco");
    c.DisplayRequestDuration();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(GeneralConstants.CORS_ORIGINS_KEY);

app.MapControllers();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.StatusCode = exception is ArgumentException ? 400 : 500;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            message = exception?.Message
        }));
    });
});

var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "images");

if (!Directory.Exists(imageDirectory))
    Directory.CreateDirectory(imageDirectory);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageDirectory),
    RequestPath = "/images"
});

app.Run();
