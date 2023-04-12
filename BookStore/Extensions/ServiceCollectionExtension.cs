using BookStore.Models.DAL;
using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DTOs.Settings;
using BookStore.Service;
using BookStore.Service.Interfaces;
using BookStore.Service.TokenGenerators;
using BookStore.Service.TokenValidators;

namespace BookStore.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((Func<IServiceProvider, Func<BookStoreContext>>)((provider) => () => provider.GetService<BookStoreContext>()));
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                    .AddScoped<IUserGroupRepository, UserGroupRepository>()
                    .AddScoped<ICartRepository, CartRepository>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<Encryptor>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<AccessTokenGenerator>()
                .AddScoped<RefreshTokenGenerator>()
                .AddScoped<RefreshTokenValidator>()
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<TokenGenerator>();
        }
    }
}
