using turtlearnMVP.Persistance;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.LoadMyPersistanceServices();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Index");
    options.Cookie = new CookieBuilder
    {
        Name = "turtlearnCookie",
        HttpOnly = true,//kullanýcýnýn js ile bizim cookie bilgilerimizi görmesini engelliyoruz
        SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldiðinde iþlensin
        SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmalý 
    };
    options.SlidingExpiration = true; //kullanýcý sitemize girdiðinde süre tanýnýyor
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(15); // 15 dakikatekrar giriþ gerekmeyecek
    options.AccessDeniedPath = new PathString("/Shared/AccessDenied"); //yetkisiz eriþim
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
