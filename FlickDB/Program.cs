using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FlickDB.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(Directory.GetCurrentDirectory());
    config.AddJsonFile("moviesettings.json", optional: true, reloadOnChange: true);
})
.UseContentRoot(Directory.GetCurrentDirectory());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Database Services
builder.Services.AddDbContext<MovieContext>();
builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();

// Settings Services
builder.Services.AddSingleton<ISettingService, SettingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
