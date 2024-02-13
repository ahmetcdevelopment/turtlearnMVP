using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Persistance;
using turtlearnMVP.Persistance.Configurations;
using turtlearnMVP.Persistance.Context;
using turtlearnMVP.WEB.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

#region DB SETTINGS
builder.Services.AddDbContext<turtlearnMVPContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentDB")));
#endregion
#region SMTP SETTINGS
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
#endregion
#region API SETTINGS
await turtlearnApiSetting.generateKey();// Api için key Generate ediyoruz.
#endregion
// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddSignalR(); //signalr modülünü devreye sok.

builder.Services.LoadMyPersistanceServices(builder.Configuration);

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

// Oturum Durumu Eklemek
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.//WithOrigins("https://localhost:44444/", "https://localhost:55555").  //İstediğimiz kadar client ekleyebiliyoruz.
         AllowAnyHeader().
         AllowAnyMethod().
         AllowCredentials();
    });
});
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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


//----------------------
app.UseCors("MyCorsPolicy");
//----------------------
app.MapHub<LiveMeetingHub>("/liveMeetingHub"); // endpoint ekliyorum.

app.Run();