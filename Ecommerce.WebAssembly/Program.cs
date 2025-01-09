using Ecommerce.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.LocalStorage;
using Blazored.Toast;

using Ecommerce.WebAssembly.Servicios.Contrato;
using Ecommerce.WebAssembly.Servicios.Implementacion;

using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7067/api/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services
   .AddBlazorise(options =>
   {
       options.Immediate = true;
   })
   .AddBootstrapProviders()
   .AddFontAwesomeIcons();

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<ICarritoServicio, CarritoServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
