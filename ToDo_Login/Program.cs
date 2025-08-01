using Microsoft.EntityFrameworkCore;
using ToDo_Login.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Config.CORS
app.UseCors(options => options.WithOrigins("http://localhost:4200") .AllowAnyMethod() .AllowAnyHeader());
#endregion Config.CORS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
