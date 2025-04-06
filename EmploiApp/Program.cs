using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmploiApp.Data;
using EmploiApp.Models;
using Microsoft.Extensions.DependencyInjection;
using EmploiApp.Models.Factory;
using EmploiApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // Activation de la résilience aux erreurs transitoires
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // nombre de tentatives maximum
            maxRetryDelay: TimeSpan.FromSeconds(30), // délai maximum entre chaque tentative
            errorNumbersToAdd: null // optionnel, pour ajouter des codes d'erreurs spécifiques
        );
    }));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Enregistrement du repository générique pour d'autres entités (optionnel)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Enregistrement du repository spécifique pour OffreEmploi
builder.Services.AddScoped<IOffreEmploiRepository, OffreEmploiRepository>();
// Enregistrement du repository spécifique pour Secteur
builder.Services.AddScoped<ISecteurRepository, SecteurRepository>();
// Enregistrement du repository spécifique pour CV
builder.Services.AddScoped<ICVRepository, CVRepository>();
// Enregistrement du repository spécifique pour Candidature
builder.Services.AddScoped<ICandidatureRepository, CandidatureRepository>();


builder.Services.AddScoped<CandidatProfileHandler>();
builder.Services.AddScoped<RecruteurProfileHandler>();
builder.Services.AddScoped<RoleProfileHandlerFactory>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
