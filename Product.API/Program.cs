using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Product.API.Extensioins;
using Product.API.Middleware;
using Product.Core.Services;
using Product.Infrastructure;
using Product.Infrastructure.Repository;
using StackExchange.Redis;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAPIRequestration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(s =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Auth Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    s.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securitySchema,
            new [] {"Bearer"}
        }
    };
    s.AddSecurityRequirement(securityRequirement);
});

builder.Services.InfrastructureConfigration(builder.Configuration);

builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
{
    var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(configure);
});

builder.Services.AddScoped<IOrderServices, OrderServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

InfrastructureRequistrarion.infrastructureConfigMiddleware(app);

app.Run();
