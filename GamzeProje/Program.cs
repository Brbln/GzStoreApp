using Business.Abstract;
using Business.Concrete;
using Business.DTOs; 
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore; 
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GamzeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DataAccess
builder.Services.AddScoped<IProductDal, EfProductDal>();
// Business
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddScoped<ICartItemService, CartItemManager>();


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
