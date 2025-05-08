
using Microsoft.EntityFrameworkCore;
using Manager_Device_Service.Domains;

namespace Manager_Device_Service.Providers
{
    public static class EntityFrameworkProvider
    {
        public static IServiceCollection AddEntityFrameworkProvider(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ManagerDeviceContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction =>
                {
                    //sqlServerOptionsAction.UseHierarchyId();
                    sqlServerOptionsAction.CommandTimeout(30);
                    //sqlServerOptionsAction.EnableRetryOnFailure(3);

                });
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
