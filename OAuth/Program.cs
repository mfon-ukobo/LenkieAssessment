using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OAuth.Configuration;
using OAuth.Database;
using Infrastructure;
using OAuth.Services;
using IdentityServerHost.Quickstart.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
    .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
    .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
    .AddInMemoryClients(InMemoryConfig.GetClients())
    .AddTestUsers(TestUsers.Users)
    //.AddAspNetIdentity<User>()
    //.AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential(); //not something we want to use in a production environment;

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

//app.UseHttpsRedirection();
app.UseIdentityServer();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
