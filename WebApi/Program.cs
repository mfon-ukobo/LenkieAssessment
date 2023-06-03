using Domain;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi;
using WebApi.Authorization;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Library API",
        Description = "A web APi for managing check out and check in of books"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = "https://localhost:7169";
        opt.Audience = "library_api";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.PublicSecure, policy => policy.RequireClaim("client_id"));
    options.AddPolicy(Scopes.ReadBooks, policy => policy.AddRequirements(new HasScopeRequirement(Scopes.ReadBooks)));
    options.AddPolicy(Scopes.WriteBooks, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.WriteBooks)));
    options.AddPolicy(Scopes.ReadUsers, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.ReadUsers)));
    options.AddPolicy(Scopes.WriteUsers, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.WriteUsers)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization(Policies.PublicSecure);

app.Run();
