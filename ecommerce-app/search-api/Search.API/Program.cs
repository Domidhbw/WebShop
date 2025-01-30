using Search.API.Models;
using Search.API.Services;
using System.Net.Http.Headers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Endpoint to get all products
app.MapGet("/products", (ProductService productService) =>
{
    return productService.GetProducts();
});

// Endpoint to add a new product
app.MapPost("/products", (Product product, ProductService productService) =>
{
    productService.AddProduct(product);
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();
