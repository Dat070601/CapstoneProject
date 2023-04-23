using AutoMapper;
using BookStore.Models.DAL;
using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DTOs.Settings;
using BookStore.Service;
using BookStore.Service.Interfaces;
using BookStore.Service.Mapping;
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
            services.AddScoped<ILogger, Logger<CartService>>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                    .AddScoped<IUserGroupRepository, UserGroupRepository>()
                    .AddScoped<IBookRepository,BookRepository>()
                    .AddScoped<ICartRepository, CartRepository>()
                    .AddScoped<ICartDetailRepository, CartDetailRepository>()
                    .AddScoped<IBookPriceRepository, BookPriceRepository>()
                    .AddScoped<IOrderDetailRepository, OrderDetailRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>()
                    .AddScoped<IStatusRepository, StatusRepository>()
                    .AddScoped<IReviewRepository, ReviewRepository>()
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IHistoryTransactionsRepository, HistoryTransactionsRepository>()
                    .AddScoped<IAuthorRepository, AuthorRepository>()
                    .AddScoped<IImageRepository, ImageRepository>()
                    .AddScoped<IAddressRepository, AddressRepository>();
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
                .AddScoped<IBookService,BookService>()
                .AddScoped<IMapperCustom, Service.Mapping.Mapper>()
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<ICartService, CartService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IReviewService, ReviewService>()
                .AddScoped<TokenGenerator>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<ISearcbService, SearchService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IAddressService, AddressService>();
        }
    }
}
