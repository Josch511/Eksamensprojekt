using Interface;
using Repository;
using DotNetEnv;
using ServerAPI.Interface;
using ServerAPI.Service;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == "Development")
{
    Env.Load(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env"));
}


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AuthenticationRepo>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICaseUpdateService, CaseUpdateService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/debug-path", (IWebHostEnvironment env) => new {
    WebRootPath = env.WebRootPath,
    ContentRootPath = env.ContentRootPath,
    UploadsExists = Directory.Exists(Path.Combine(env.WebRootPath ?? "", "uploads"))
});

app.Run();
