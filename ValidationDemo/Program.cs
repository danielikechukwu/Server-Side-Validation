using Microsoft.AspNetCore.Mvc;
using ValidationDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                // Override the default invalid model state response
                // InvalidModelStateResponseFactory: This delegate is invoked when model validation fails.
                // By overriding it, we can control the structure and content of the validation error responses.
                options.InvalidModelStateResponseFactory = context =>
                {
                    // Extract the error messages from the model state
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(e => new FieldError
                        {
                            Field = e.Key,
                            // Option 1: Use only the first error message
                            // Error = e.Value.Errors.FirstOrDefault()?.ErrorMessage
                            // Join multiple error messages into a single string separated by semicolons
                            Error = string.Join("; ", e.Value?.Errors.Select(x => x.ErrorMessage ?? string.Empty) ?? Array.Empty<string>())
                        }).ToList();
                    // Create a custom error response object
                    var errorResponse = new ValidationErrorResponse
                    {
                        //The HTTP status code (400 for Bad Request).
                        StatusCode = 400,
                        // A general message indicating that validation failed.
                        Message = "Validation Failed",
                        // An array containing details about each validation error, including the field name and associated error messages.
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse)
                    {
                        // Ensures the response is returned as application/json.
                        ContentTypes = { "application/json" }
                    };
                };
            })
.AddJsonOptions(options =>
{
    // Use the property names as defined in the C# model
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

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
