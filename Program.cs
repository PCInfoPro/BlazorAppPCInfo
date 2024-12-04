using BlazorAppPCInfo.Components;
using Blazored.Modal;
using INFOPC.Services;
using NLog; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConfigurationService>(builder.Configuration);

new APIService(builder.Configuration.GetSection("API").Value);
new BlobApiService(builder.Configuration.GetSection("BlobAPI").Value);

builder.Services.AddLocalization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

// builder.Logging.AddConfiguration(
//     builder.Configuration.GetSection("NLog"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredModal();

builder.Services.AddServerSideBlazor();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddControllers();


var app = builder.Build();


var supportedCultures = new[] { "en-US", "es-ES" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
