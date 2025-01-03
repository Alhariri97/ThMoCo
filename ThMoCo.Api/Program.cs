using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}/",
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidateLifetime = true,
            RoleClaimType = "permissions"
        };

    });

builder.Services.AddAuthorization();


//// Configure services based on environment
//if (builder.Environment.IsDevelopment())
//{
//    Console.WriteLine("Registering LocalProductService as IProductService");
//    builder.Services.AddSingleton<IProductService, LocalProductService>();
//    builder.Services.AddSingleton<IProfileService, LocalProfileService>();
//    builder.Services.AddSingleton<IOrderService, LocalOrderService>();
//}
//else
//{
//    //    //Console.WriteLine("Registering ProductService as IProductService");
//    //    //builder.Services.AddDbContext<ProductsContext>(options =>
//    //    //{
//    //    //    var cs = builder.Configuration.GetConnectionString("ConnectionString");
//    //    //    options.UseSqlServer(cs);
//    //    //});
//    //    //builder.Services.AddScoped<IProductService, ProductService>();


//    Console.WriteLine("Registering ProductService as IProductService");
//    builder.Services.AddDbContext<AppDbContext>(options =>
//    {
//        var cs = builder.Configuration.GetConnectionString("ConnectionString");
//        options.UseSqlServer(cs);
//    });
//    builder.Services.AddScoped<IProductService, ProductService>();
//    builder.Services.AddScoped<IProfileService, ProfileService>();
//    builder.Services.AddScoped<IOrderService, OrderService>();
//}

////////////////////////////////////
Console.WriteLine("Registering ProductService as IProductService");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("ConnectionString");
    options.UseSqlServer(cs);
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IOrderService, OrderService>();

////////////////////////////////////////


var app = builder.Build();

if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwaggerInProduction"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");

    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/protected", () => "You are authorized!")
   .RequireAuthorization();


app.MapControllers();

app.Run();

namespace ThMoCo.Api
{
    public partial class Program { }
}