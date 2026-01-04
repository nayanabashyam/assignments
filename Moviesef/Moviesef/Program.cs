using Microsoft.EntityFrameworkCore;
using Movies_EF.Endpoints;
using Movies_EF.Persistence;
using Movies_EF.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MovieDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
builder.Services.AddTransient<IMovieService, MovieService>();

var app = builder.Build();

// 1. Database initialization FIRST (before any HTTP handling)
await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>())
{
    await dbContext.Database.EnsureCreatedAsync();
}

// 2. Development-only OpenAPI/Scalar middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// 3. Standard middleware pipeline (order is CRITICAL)
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();  // Controllers must come before minimal API endpoints

// 4. Custom endpoints LAST
app.MapGet("/", () => "Hello World!")
   .Produces(200, typeof(string));

app.MapMovieEndpoints();

app.Run();
