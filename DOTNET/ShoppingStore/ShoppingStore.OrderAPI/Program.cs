using Microsoft.EntityFrameworkCore;
using ShoppingStore.OrderAPI;
using ShoppingStore.OrderAPI.Data;
using ShoppingStore.OrderAPI.Data.Repositories;
using ShoppingStore.OrderAPI.Interfaces;
using ShoppingStore.OrderAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlite(connectionString: "Data Source=orders-database.db");
});

builder.Services.AddScoped<IRepository<Order>, OrdersRepository>();



builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
context.Database.EnsureCreated();

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
