using Manager_Device_Service.Repositories.Implement;
using Manager_Device_Service.Repositories.Interface.ISeedWorks;
using Manager_Device_Service.Services;
using Manager_Device_Service.Services.Interfaces;

namespace Manager_Device_Service.Providers
{
    public static class ScopedProvider
    {


        public static IServiceCollection AddScopedProvider(this IServiceCollection services)
        {

            var servicesR = typeof(BuildingRepository).Assembly.GetTypes()
            .Where(x => x.GetInterfaces().Any(i => i.Name == typeof(IRepositoryBase<,>).Name) && !x.IsAbstract && x.IsClass && !x.IsGenericType);

            foreach (var service in servicesR)
            {
                var allInterfaces = service.GetInterfaces();
                var directInterface = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces())).FirstOrDefault();
                if (directInterface != null)
                {
                    services.Add(new ServiceDescriptor(directInterface, service, ServiceLifetime.Scoped));
                }
            }


            services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))




            .AddScoped<IFileService, FileService>()
            .AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBorrowReminderService, BorrowReminderService>();
            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }

}
