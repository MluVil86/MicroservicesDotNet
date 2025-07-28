using ProductsService.BusinessLogicLayer;
using ProductsService.DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsService.API.Middleware;
using ProductsService.API.APIEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddDataBusinessLayer();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader(); ;
    });
});


var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//cors
app.UseCors();

//swagger
app.UseSwagger();
app.UseSwaggerUI();

//auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndPoints();

app.Run();
