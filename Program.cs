using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using OperationCHAN.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OperationCHAN.Areas.Identity.Services;
using OperationCHAN;
using OperationCHAN.Models;

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

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddDiscord(options =>
    {
        options.AuthorizationEndpoint = "https://discord.com/oauth2/authorize";
        options.Scope.Add("identify");
        options.Scope.Add("email");
        
        options.CallbackPath = new PathString("/signin-discord");
        
        options.ClientId = "1037686187588067419";
        options.ClientSecret = "SIenibsqkRxwigs_ChMg41OmmqOxjS2v";
        
        options.TokenEndpoint= "https://discord.com/api/oauth2/token";
        options.UserInformationEndpoint = "https://discord.com/api/users/@me";
        
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        
        options.AccessDeniedPath = "/Home/DiscordAuthFailed";

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                
                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();
                
                var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                
                context.RunClaimActions(user);
            }
        };
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

new Timeedit(app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>()).StartLoop();

app.Run();