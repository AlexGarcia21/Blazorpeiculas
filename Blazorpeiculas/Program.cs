using Blazorpeiculas;
using Blazorpeiculas.Components;
using Blazorpeiculas.Components.Account;
using Blazorpeiculas.Constantes;
using Blazorpeiculas.Datos;
using Blazorpeiculas.Entidades;
using Blazorpeiculas.Politicas;
using Blazorpeiculas.Servicios;
using BlazorpeiculasComponents.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro de servicios
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})

    //.AddMicrosoftAccount(options =>
    //{
    //    options.ClientId = builder.Configuration["MicrosoftClientId"]!;
    //    options.ClientSecret = builder.Configuration["MicrosoftSecretId"]!;

    //    options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
    //    options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
    //})
    .AddIdentityCookies();








builder.Services.AddAuthorization(opciones =>
{
opciones.AddPolicy("PuedeEditarRoles", politica =>
{
    politica.RequireAssertion(contexto =>
    {
        return contexto.User.IsInRole(Roles.ROL_ADMIN) ||
               contexto.User.HasClaim(c => c.Type == "superadmin");
    });
});

opciones.AddPolicy("PuedeEditarRolesDB", policy =>
    {
    policy.AddRequirements(new PuedeEditarRolesRequirement());

});





});

builder.Services.AddScoped<IAuthorizationHandler, PuedeEditarRolesHandler>();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Desactivado para desarrollo local
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
})
.AddRoles<IdentityRole>()
.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSeeding((context, _) =>
    {
        var rolAdmin = "administrador";
        var roles = context.Set<IdentityRole>();
        var existeRolAdmin = roles.Any(r => r.Name == rolAdmin);

        if (!existeRolAdmin)
        {
            roles.Add(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = rolAdmin,
                NormalizedName = rolAdmin.ToUpperInvariant()
            });

            context.SaveChanges();
        }
    })
    .UseAsyncSeeding(async (context, _, ct) =>
    {
        var rolAdmin = "administrador";
        var roles = context.Set<IdentityRole>();
        var existeRolAdmin = await roles.AnyAsync(r => r.Name == rolAdmin, cancellationToken: ct);

        if (!existeRolAdmin)
        {
            roles.Add(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = rolAdmin,
                NormalizedName = rolAdmin.ToUpperInvariant()
            });

            await context.SaveChangesAsync(ct);
        }
    }));

builder.Services.AddScoped<IServicioPeliculas, ServicioPeliculas>();
builder.Services.AddScoped<IServiciosGeneros, ServicioGeneros>();
builder.Services.AddScoped<IServicioActores, ServicioActores>();
builder.Services.AddScoped<IServicioSeguridad, ServicioSeguridad>();
builder.Services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IServicioVoto, ServicioVotos>();
builder.Services.AddTransient<IEmailSender, ServicioCorreos>();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (dbcontext.Database.IsRelational())
    {
        dbcontext.Database.Migrate();
    }
}

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();