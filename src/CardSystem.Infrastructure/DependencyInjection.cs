using CardSystem.Application.Common.Interfaces;
using CardSystem.Domain.Settings;
using CardSystem.Infrastructure.Services;
using CardSystem.Persistence.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CardSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IWebHostEnvironment environment, IConfiguration config)
        {

            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication();

            return services;
        }
    }
}
