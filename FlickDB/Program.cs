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
builder.Services.AddSingleton<IDbConnection, DbConnection>();

// Settings Services
builder.Services.AddSingleton<ISettingReader, SettingService>();

// Movie Form Services
builder.Services.AddScoped<MovieForm>();

// Actor Form Services
builder.Services.AddScoped<ActorForm>();

// Crew Form Services
builder.Services.AddScoped<CrewForm>();

// Genre Form Services
builder.Services.AddScoped<GenreForm>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else if (app.Environment.IsStaging())
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Error/Staging/Error");
}
else
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Error/Production/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
