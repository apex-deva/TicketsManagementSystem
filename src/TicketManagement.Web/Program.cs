using TicketManagement.Application.Queries;
using TicketManagement.Web;
using Microsoft.Extensions.FileProviders;
using System.IO;
using TicketManagement.Web.Hubs;
using TicketManagement.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblyContaining<GetTicketsQuery>());

// Only add Swagger in development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
builder.Services.AddSignalR();
builder.Services.AddHostedService<TicketAutoHandlerService>();

builder.Services.AddStartupDi(builder.Configuration);
builder.Services.AddControllers();

var allowedOrigins = builder.Environment.IsDevelopment() 
    ? ["http://localhost:4200" , "http://localhost:57507"]
    : new[] { "https://yourproductiondomain.com" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer",
        x => x
            .WithOrigins(allowedOrigins) // Angular dev server
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebSockets();
}
app.UseCors("AllowAngularDevServer");
// Configure static files from Angular dist folder FIRST
var angularDistPath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist/browser");
var angularDistExists = Directory.Exists(angularDistPath);

if (angularDistExists)
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(angularDistPath),
        RequestPath = ""
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapHub<TicketHub>("/tickethub"));
// app.MapHub<TicketHub>("/ticketHub"); // Add this line

app.UseAuthorization();
app.MapControllers();
// Configure Swagger only in development and only under /swagger
if (app.Environment.IsDevelopment())
{
    // Proxy requests to Angular dev server
    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.StartsWithSegments("/api") && 
            !context.Request.Path.StartsWithSegments("/swagger"))
        {
            context.Response.Redirect($"http://localhost:4200{context.Request.Path}");
            return;
        }
        await next();
    });
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Production configuration for serving built Angular files
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist/browser")),
        RequestPath = ""
    });
    
    app.MapFallbackToFile("index.html");
}

// Fallback to Angular app - THIS MUST COME AFTER SWAGGER CONFIGURATION
if (angularDistExists)
{
    app.MapFallbackToFile("index.html", new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(angularDistPath)
    });
}

app.Run();