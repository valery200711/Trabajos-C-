using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar DbContext
builder.Services.AddDbContext<TechNovaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion") ??
    throw new InvalidOperationException("Connection string 'conexion' not found.")));

// Registrar Identity completo con roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TechNovaDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Configurar cookies de autenticación (opcional)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Seed: crear rol Administrador y usuario inicial
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Crear rol Administrador si no existe
    if (!await roleManager.RoleExistsAsync("Administrador"))
    {
        await roleManager.CreateAsync(new IdentityRole("Administrador"));
    }

    // Crear usuario admin inicial si no existe
    var adminEmail = "admin@technova.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        // Crear usuario con contraseña inicial
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Administrador");
    }
}

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
