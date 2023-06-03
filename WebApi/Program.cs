using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using WebApi;
using WebApi.Authorization;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = "https://localhost:7169";
        opt.TokenValidationParameters.ValidateAudience = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Scopes.ReadBooks, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.ReadBooks)));
    options.AddPolicy(Scopes.WriteBooks, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.WriteBooks)));
    options.AddPolicy(Scopes.ReadUsers, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.ReadUsers)));
    options.AddPolicy(Scopes.WriteUsers, policy => policy.Requirements.Add(new HasScopeRequirement(Scopes.WriteUsers)));
});

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

app.MapControllers();

app.Run();
