global using Domain.DTO;
global using Application.Interfaces;
global using Infrastructure.Repositories;
global using Infrastructure.Data;

using API.Services;

string CustomPolicy = "customPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.GetApplicationServices(builder.Configuration, builder.Environment);

builder.Services.AddCors(opt => opt.AddPolicy(CustomPolicy, builder => {
	builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
}));

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
app.UseAuthentication();
app.UseCors(CustomPolicy);
app.UseAuthorization();

app.MapControllers();

app.Run();
