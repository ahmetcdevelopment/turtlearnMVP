using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Persistance;
using turtlearnMVP.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//Database
builder.Services.AddDbContext<turtlearnMVPContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentDB")));

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.LoadMyPersistanceServices();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false; // şifrede rakam bulunsun mu?
    options.Password.RequiredLength = 5; // şifre en az kaç karakterden oluþsun?
    options.Password.RequiredUniqueChars = 0; // unique karakterlerden kaç tane olsun
    options.Password.RequireNonAlphanumeric = false;// küçük karakterler zorunlu kýlýnsýn mý?
    options.Password.RequireUppercase = false;// büyük karakterler zorunlu kýlýnsýn mý?

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
    options.User.RequireUniqueEmail = true; // tüm emailler eþsiz olsun mu?

}).AddEntityFrameworkStores<turtlearnMVPContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Index");
    options.Cookie = new CookieBuilder
    {
        Name = "turtlearnCookie",
        HttpOnly = true,//kullanıcının js ile bizim cookie bilgilerimizi görmesini engelliyoruz
        SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldiğinde işlensin
        SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmalı 
    };
    options.SlidingExpiration = true; //kullanıcı sitemize girdiğinde süre tanınıyor
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(15); // 15 dakikatekrar giriş gerekmeyecek
    options.AccessDeniedPath = new PathString("/Shared/AccessDenied"); //yetkisiz erişim
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();