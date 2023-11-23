using Core_Web_Project.Models;
using Core_Web_Project.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UygulamaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();//Razor yapısını kullanabilmemiz için eklenmeli.

// �NEML�! Yeni bir Repository s�n�f� olu�turuldu�unda mutlaka burada Services'lere eklenmelidir.
// _kitapTuruRepository nesnesi => Dependency Injection Yap�s�
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

// Scoped(AddScoped): Nesnenin request sonlanana kadar kullan�lmas�n� farkl� t�rden bir �a�r� i�in geldi�inde yeni bir nesne olu�turulmas�n� sa�lar.

builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages(); //Razor yapısını kullanabilmemiz için eklenmeli.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
 