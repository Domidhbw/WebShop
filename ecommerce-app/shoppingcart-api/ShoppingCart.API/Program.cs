using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Services;
using ShoppingCart.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint to get a shopping cart by username
app.MapGet("/shoppingcart/{username}", (string username, ShoppingCartService shoppingCartService) =>
{
    var cart = shoppingCartService.GetShoppingCartByUser(username);
    return cart is not null ? Results.Ok(cart) : Results.NotFound("User not found");
});

app.MapPost("/shoppingcart", ([FromBody] ShoppingCart cart, ShoppingCartService shoppingCartService) =>
{
    shoppingCartService.AddOrUpdateShoppingCart(cart);
    return Results.Ok("Shopping cart updated successfully");
});

app.Run();


