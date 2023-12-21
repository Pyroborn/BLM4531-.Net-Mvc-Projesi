using Microsoft.AspNetCore.Authentication.Cookies;
using Mysitemvc.Models;
using Mysitemvc.Services;

var builder = WebApplication.CreateBuilder(args);
// Other using statements...


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor(); //bu line ekledim
builder.Services.AddScoped<SecurityService>(); // bu line ekledim

//var app = builder.Build(); burda vardý taþýdým

//burasý eklenti:
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
{
    opts.Cookie.Name = "app_auth";
    opts.ExpireTimeSpan = TimeSpan.FromDays(7);
    opts.SlidingExpiration = false;
    opts.LoginPath = "/login/LoginSuccess";
    opts.LogoutPath = "/login/Logout";
    opts.AccessDeniedPath = "/login/LoginFailure";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Other using statements...

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // bu kýsým =Home ve =Indexti. Login path ile ayný deðer yaptýk gibi ayrýca app.useAuth eklendi ve cookie eklendi

app.Run();
