using MVCVentas.Utilidades.Automapper;
using SistemaVenta.IOC;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(op=>op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    opt =>
    {
        opt.LoginPath = "/Acceso/Loggin"; opt.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
builder.Services.InyectarDependencia(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
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
    pattern: "{controller=Acceso}/{action=Loggin}/{id?}");

app.Run();
