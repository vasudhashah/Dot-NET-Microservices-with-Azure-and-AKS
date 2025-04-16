using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Infrastructure Services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

//Add controllers to the services collection
//Adding AddJsonOptions to convert Json Object to string
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(typeof (ApplicationUserMappingProfile).Assembly);

//FluentValidations
builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

//Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Adds endpoint that can serve the swagger.json
app.UseSwagger();

//Adds Swagger UI
app.UseSwaggerUI();

//Add CORS so that frontend application can access our endpoints
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();


//controller routes
app.MapControllers();

app.Run();

