using BookStore.Models.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();

var defaultConnectionString = string.Empty;
defaultConnectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<BookStoreContext>(
    options =>
    {
        options.UseSqlServer(defaultConnectionString, b => b.MigrationsAssembly("BookStore"));
        options.UseLazyLoadingProxies();
    }
);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
