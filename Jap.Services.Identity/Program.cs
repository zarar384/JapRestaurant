using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Services;
using Jap.Services.Identity;
using Jap.Services.Identity.DbContexts;
using Jap.Services.Identity.Initializer;
using Jap.Services.Identity.Models;
using Jap.Services.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbConext>(options =>
    options.UseSqlServer(connectionString));

//Identity. Use Token instead of password
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbConext>()
    .AddDefaultTokenProviders();

//Identity Server
var identityBuilder = builder.Services.AddIdentityServer(option =>
{
    //generation events: error processing, event information, was launched successfully or failed
    option.Events.RaiseErrorEvents = true;
    option.Events.RaiseInformationEvents = true;
    option.Events.RaiseFailureEvents = true;
    option.Events.RaiseSuccessEvents = true;
    //generation of static requirements
    option.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.IdentityResources) //add api resources in memory, scopes and client 
  .AddInMemoryApiScopes(SD.ApiScopes)
  .AddInMemoryClients(SD.Clients)
  .AddAspNetIdentity<ApplicationUser>();//identification using the User

//Add Initializer
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

//special data for subscription. Should store the key
//automatically generate key for easy development phase
identityBuilder.AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

//add IDbInitializer to configure 
using (var serviceScope = app.Services.CreateScope())
{
    var service = serviceScope.ServiceProvider.GetService<IDbInitializer>();
    service.Initialize();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
