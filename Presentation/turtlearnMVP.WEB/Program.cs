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
        HttpOnly = true,//kullan�c�n�n js ile bizim cookie bilgilerimizi g�rmesini engelliyoruz
        SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldi�inde i�lensin
        SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmal� 
    };
    options.SlidingExpiration = true; //kullan�c� sitemize girdi�inde s�re tan�n�yor
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(15); // 15 dakikatekrar giri� gerekmeyecek
    options.AccessDeniedPath = new PathString("/Shared/AccessDenied"); //yetkisiz eri�im
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
