using OperationCHAN.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Areas.Identity.Services;
using OperationCHAN;
using OperationCHAN.Models;
using Microsoft.AspNetCore.ResponseCompression;
using OperationCHAN.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<StudentUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(opts => { opts.SignIn.RequireConfirmedEmail = true; });

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAuthentication().AddDiscord(options =>
{
    options.Scope.Add("identify");
    options.Scope.Add("email");
    options.ClientId = "1037686187588067419";
    options.ClientSecret = "SIenibsqkRxwigs_ChMg41OmmqOxjS2v";
    options.SaveTokens = true;
    options.AccessDeniedPath = "/Home/DiscordAuthFailed";
});

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews().AddRazorPagesOptions(options => {
    options.Conventions.AddAreaPageRoute("Ticket", "/Create", "");
});

var app = builder.Build();

using (var services = app.Services.CreateScope())
{
    var db = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var um = services.ServiceProvider.GetRequiredService<UserManager<StudentUser>>();
    var rm = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    ApplicationDbInitializer.Initialize(db, um, rm); 
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapHub<HelplistHub>("/chatHub");

// Route added for debugging purposes, to see all available endpoints
app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

// Start TimeEdit loop
new Timeedit(app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>()).StartLoop();

// Route added for debugging purposes, to see all available endpoints
app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

// Start TimeEdit loop
new Timeedit(app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>()).StartLoop();

app.Run();