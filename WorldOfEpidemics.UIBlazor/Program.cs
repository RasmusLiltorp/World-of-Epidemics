using WorldOfEpidemics.Core;
using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.UIBlazor;
using WorldOfEpidemics.UIBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IUserController, UserController>();
builder.Services.AddSingleton((serviceProvider) => (UserController)serviceProvider.GetRequiredService<IUserController>());
builder.Services.AddSingleton<Game>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
