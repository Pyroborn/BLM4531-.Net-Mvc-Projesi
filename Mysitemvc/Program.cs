using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Routing;
using Mysitemvc.Controllers;
using Mysitemvc.Models;
using Mysitemvc.Services;

var builder = WebApplication.CreateBuilder(args);
// Other using statements...




// Add services to the container.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your desired timeout
});

builder.Services.AddSingleton<TokenValidationService>();//token valiserv
builder.Services.AddScoped<TokenValidationService>();// scoped token 
builder.Services.AddScoped<UsersDAO>();// usersDao scoped
builder.Services.AddScoped<UsersController>();// userscontroller
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor(); //bu line ekledim
builder.Services.AddScoped<SecurityService>(); // bu line ekledim
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie(options =>
   // {
    //    options.Cookie.Name = "app_auth";
     //   options.ExpireTimeSpan = TimeSpan.FromDays(7);
     //   options.SlidingExpiration = false;
      //  options.LoginPath = "/login/LoginSuccess";
     //   options.LogoutPath = "/login/Logout";
     //   options.AccessDeniedPath = "/login/LoginFailure";
     //   options.Cookie.SameSite = SameSiteMode.Lax; //or Strict
     //   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
   // });

//builder.Services.AddAuthorization(options =>
//{
 //   options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//});

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
app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // bu kýsým =Home ve =Indexti. Login path ile ayný deðer yaptýk gibi ayrýca app.useAuth eklendi ve cookie eklendi

app.Run();
