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

//// Configure services based on environment
//if (builder.Environment.IsDevelopment())
//{
//    Console.WriteLine("Registering LocalProductService as IProductService");
//    builder.Services.AddSingleton<IProductService, LocalProductService>();
//}
//else
//{
//    Console.WriteLine("Registering ProductService as IProductService");
//    builder.Services.AddDbContext<ProductsContext>(options =>
//    {
//        var cs = builder.Configuration.GetConnectionString("ConnectionString");
//        options.UseSqlServer(cs);
//    });
//    builder.Services.AddScoped<IProductService, ProductService>();
//}


Console.WriteLine("Registering ProductService as IProductService");
builder.Services.AddDbContext<ProductsContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("ConnectionString");
    if (string.IsNullOrEmpty(cs))
    {
        Console.WriteLine("Connection string not found or is empty.");
        throw new InvalidOperationException("Database connection string is not configured.");
    }
    else
    {
        Console.WriteLine("Connection string successfully loaded.");
    }

    options.UseSqlServer(cs);
});

builder.Services.AddScoped<IProductService, ProductService>();



///////////////////////


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
