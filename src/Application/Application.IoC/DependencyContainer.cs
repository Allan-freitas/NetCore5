using Application.App.CommandHandler;
using Application.App.Commands.Users;
using Application.App.Queries.Users;
using Application.App.QueryHandler;
using Application.Bus;
using Application.Data.Context;
using Application.Data.Repositories;
using Application.Domain.Core.Bus;
using Application.Domain.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //mediator
            services.AddScoped<IEventBus, MediatorHandler>();

            //commands
            services.AddScoped<IRequestHandler<LoginUserCommand, ResponseResult>, LoginUserHandler>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, ResponseResult>, RegisterUserHandler>();

            //Queries
            services.AddScoped<IRequestHandler<UserQuery, ResponseResult>, ListUserHandler>();

            //repositórios
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
