using Microsoft.AspNetCore.Mvc;
using ShoppingCartDB.API.Models;
using ShoppingCartDB.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ShoppingCartService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/shoppingcart/{username}", (string username, [FromServices]ShoppingCartService shoppingCartService) =>
{
    var cart = shoppingCartService.GetShoppingCartByUser(username);
    return cart is not null ? Results.Ok(cart) : Results.NotFound("User not found");
});


app.MapPost("/shoppingcart", ([FromBody]ShoppingCart cart,[FromServices] ShoppingCartService shoppingCartService) =>
{
    shoppingCartService.AddOrUpdateShoppingCart(cart);
    return Results.Ok("Shopping cart updated successfully");
});

app.Run();

