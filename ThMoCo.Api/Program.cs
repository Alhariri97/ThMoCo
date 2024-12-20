using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.Data;
using ThMoCo.Api.IServices;
using ThMoCo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation in development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
