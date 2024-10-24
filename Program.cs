using BlazorAppPCInfo.Components;
using INFOPC.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar cliente HTTP
builder.Services.AddHttpClient<ComputerService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5175/api"); // URL base de tu API
});
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();

// builder.Services.AddScoped<ComputerService>();




builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseEndpoints(endpoints =>
//     {
//         endpoints.MapRazorPages();
//         endpoints.MapBlazorHub();
//         endpoints.MapFallbackToPage("/_Host");
//     });

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
