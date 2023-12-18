using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// services.AddDbContext<ApplicationDbContext>(options =>
// options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// Add your services here
builder.Services.AddScoped<IRepository<Post>, Repository<Post>>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Other middleware and configurations
app.UseHttpsRedirection();
app.UseStaticFiles();

// Your application routes and endpoints
// Routing = URL + HTTP Method
app.UseRouting(); // Enable routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();