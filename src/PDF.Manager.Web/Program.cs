using PDF.Manager.Application.DependencyInjection;
using PDF.Manager.Infrastructure.DependencyInjection;
using PDF.Manager.Web.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Presentation: Blazor Server with interactive server-side rendering.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

// Composition root: wire the inner layers into the dependency injection container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
