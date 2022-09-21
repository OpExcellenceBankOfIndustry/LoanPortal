//using Blazored.LocalStorage;
using Blazored.Modal;
using BOILoanPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/register";
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthenticationCore();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IAccountOpeningService, AccountOpeningService>();
builder.Services.AddTransient<IUtilityService, UtilityService>();
builder.Services.AddTransient<IValidationService, ValidationService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<BOILoanPortal.Services.ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredModal();
builder.Services.AddSingleton<CircuitHandler>(new CircuitHandlerService());

//builder.Services.AddScoped<AuthenticationState>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
//app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
