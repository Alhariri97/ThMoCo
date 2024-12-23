using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ThMoCo.Api.Data;
using ThMoCo.Api.IServices;
using ThMoCo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation in development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Identifier"];
    });

builder.Services.AddAuthorization();


// Configure services based on environment
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Registering LocalProductService as IProductService");
    builder.Services.AddSingleton<IProductService, LocalProductService>();
}
else
{
    Console.WriteLine("Registering ProductService as IProductService");
    builder.Services.AddDbContext<ProductsContext>(options =>
    {
        var cs = builder.Configuration.GetConnectionString("ConnectionString");
        options.UseSqlServer(cs);
    });
    builder.Services.AddScoped<IProductService, ProductService>();
}

//////////////////////////////////////
//Console.WriteLine("Registering ProductService as IProductService");
//builder.Services.AddDbContext<ProductsContext>(options =>
//{
//    var cs = builder.Configuration.GetConnectionString("ConnectionString");
//    options.UseSqlServer(cs);
//});
//builder.Services.AddScoped<IProductService, ProductService>();
//////////////////////////////////////////


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwaggerInProduction"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Optional: Set up Swagger UI to be accessible under a custom path, for instance:
        // c.RoutePrefix = "docs"; // This will make it available at /docs
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");

        // Optional: Enable authentication in the Swagger UI
        // c.DefaultModelsExpandDepth(-1); // Optional, disable models expansion for better security
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();

// Explicitly define the Program class for integration testing purposes
namespace ThMoCo.Api
{
    public partial class Program { }
}