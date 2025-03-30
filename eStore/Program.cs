using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using eStore.Components;
using eStore.Services.Interfaces;
using eStore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var conString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<eStoreDuckContext>(options => options.UseSqlServer(conString));
builder.Services.Configure<AdminAccountSettings>(builder.Configuration.GetSection("AdminAccount"));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();

// Configure Distributed Cache (for session persistence across SignalR connections)
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();
// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/login");
        return;
    }
    await next();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
