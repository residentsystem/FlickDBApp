var builder = WebApplication.CreateBuilder(args);

//if (!File.Exists("moviesettings.json"))
//{
//    throw new SettingFileNullReferenceException();
//}

builder.Configuration.AddJsonFile("moviesettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Database Services
builder.Services.AddDbContextFactory<MovieContext>();
builder.Services.AddSingleton<IDatabaseConnector, DatabaseService>();

// Settings Services
builder.Services.AddSingleton<ISettingReader, SettingService>();

// Movie Form Services
builder.Services.AddScoped<MovieForm>();

// Actor Form Services
builder.Services.AddScoped<ActorForm>();

// Crew Form Services
builder.Services.AddScoped<CrewForm>();

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
