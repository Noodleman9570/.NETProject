using Ecommerce.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.Bootstrap;
using Blazored.LocalStorage;
using Blazored.Toast;

using Ecommerce.WebAssembly.Servicios.Contrato;
using Ecommerce.WebAssembly.Servicios.Implementacion;

using CurrieTechnologies.Razor.SweetAlert2;

using Microsoft.AspNetCore.Components.Authorization;
using Ecommerce.WebAssembly.Extensiones;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7067/api/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<ICarritoServicio, CarritoServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();

builder.Services.AddSweetAlert2();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();

builder.Services
      .AddBlazorise(options =>
      {
          options.Immediate = true;
      })
      .AddBootstrapProviders()
      .AddBootstrapIcons();

var app = builder.Build();

// Eliminar las siguientes l�neas ya que no son necesarias y causan el error
// app.Services
//    .UseBootstrapProviders()
//    .UseBootstrapIcons();

await app.RunAsync();
