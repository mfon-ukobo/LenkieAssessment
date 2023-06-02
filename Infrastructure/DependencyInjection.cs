using Application.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<UnitOfWork>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
            services.AddDbContext<DatabaseContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("Database")));
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
        }
    }
}
