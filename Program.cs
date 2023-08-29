using Microsoft.EntityFrameworkCore;
using NumberAPI.Data;
using NumberAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Numbers"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("api/numbers", async (AppDbContext context) =>
{
    var items = await context.Numbers.ToListAsync();

    return Results.Ok(items);
});

app.MapPost("api/numbers", async (AppDbContext context, NumItem numItem) =>
{
    await context.Numbers.AddAsync(numItem);

    await context.SaveChangesAsync();

    return Results.Created($"api/numbers/{numItem.Id}", numItem);
});

app.Run();