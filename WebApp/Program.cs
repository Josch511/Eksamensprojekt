using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using WebApp.Service;
using WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICreateCaseService, CreateCaseService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:7023/"
    : "https://gruppe3-api.azurewebsites.net/";

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

await builder.Build().RunAsync();
