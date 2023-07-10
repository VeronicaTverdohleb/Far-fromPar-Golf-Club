using BlazorApp.Auth;
using BlazorApp.Services;
using BlazorApp.Services.Http;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IGameService, GameHttpClient>();
builder.Services.AddScoped<IScoreService, ScoreHttpClient>();
builder.Services.AddScoped<ITournamentService, TournamentHttpClient>();
builder.Services.AddScoped<IEquipmentService, EquipmentHttpClient>();
builder.Services.AddScoped<IJavaSocketConnection, JavaSocketConnection>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IUserService, UserHttpClient>();

AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7042") 
        }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();