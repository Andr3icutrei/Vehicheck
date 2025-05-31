using Vehicheck.Core;
using Vehicheck.Database;
using Vehicheck.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

AppConfig.Init(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddRepositories();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicheck API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
