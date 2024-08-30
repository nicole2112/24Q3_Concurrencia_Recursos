using Microsoft.EntityFrameworkCore;
using ShoppingStore.ProductAPI;
using ShoppingStore.ProductAPI.Data;
using ShoppingStore.ProductAPI.Data.Repositories;
using ShoppingStore.ProductAPI.Interfaces;
using ShoppingStore.ProductAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlite(connectionString: "Data Source=products-database.db");
});

builder.Services.AddScoped<IRepository<Product>, ProductsRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
context.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
