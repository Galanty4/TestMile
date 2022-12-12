using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Validators;
using DAL.Repositories;
using DAL.TestMilDbContext;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Internal;

namespace API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestMilDBcontext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetSection("ConnectionStrings")["TestMilDatabase"]);
            });
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserDto>, UserValidator>();
        }
    }
}
