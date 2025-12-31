using Business;
using Core.Abstracts.IServices;
using Core.Concretes.Enums;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// builder alaný: Hazýrladýðýmýz uygulamanýn baþlamadan önce iþletilen yapýlandýrma alanýdýr. Bu alanda kütüphanelerin, servislerin tanýmlamalarý yapýlýr. Factory Design Patter düzeni burada saðlanýr.

builder.Services.AddControllersWithViews();

// Business katmanýndan gelen dependency injection ayarlarý.
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

// app alaný: Yapýladýrýlmýþ uygulama kullanýcýdan gelen talebi (request) burada iþler, sýrasýyla aþaðýdaki adýmlara bu talebi yollar, bu adýmlarýn tamamýna "Middleware" denir. Her yapýný belirli bir sýrada olmasý gerekebilir.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik yönetimi sayfa yetki kontrolünden önce gelmek zorundadýr.
app.UseAuthorization(); // Eriþim yönetimi.


// Basit iþlemler için endpoint API tanýmlamasý yapýlabilir. Tam anlamýyla bir API yapýsý olmasa da, belirli controller ve actionlara doðrudan eriþim için kullanýlýr.

app.MapPost("/api/leads/pick/{id}", async (ILeadService service, ClaimsPrincipal user, int id) => await service.PickLeadAsync(id, user));

app.MapPost("/api/leads/addactivity/{type}/{lead_id}", async (ILeadService service, ClaimsPrincipal user, ActivityType type, int lead_id) => await service.AddActivityAsync(type, lead_id, user));

// routeconfig.cs buraya geldi.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Gelen talebe ilgili response döndürülür.
