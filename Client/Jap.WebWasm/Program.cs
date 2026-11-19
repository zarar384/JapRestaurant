using Jap.WebWasm;
using Jap.WebWasm.Models;
using Jap.WebWasm.Services;
using Jap.WebWasm.Services.IServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Read service base URLs from configuration and populate SD
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"] ?? string.Empty;
SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"] ?? string.Empty;
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"] ?? string.Empty;
SD.IdentityAPIBase = builder.Configuration["ServiceUrls:IdentityAPI"] ?? string.Empty;

// Named HttpClient that attaches the OIDC bearer token automatically
builder.Services.AddHttpClient("JapApi", client =>
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[]
            {
                SD.ProductAPIBase,
                SD.ShoppingCartAPIBase,
                SD.CouponAPIBase
            }));

// Default HttpClient (used by components that don't need auth)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// OIDC authentication (reads OidcConfiguration from appsettings.json)
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
});

// Application services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();

await builder.Build().RunAsync();

