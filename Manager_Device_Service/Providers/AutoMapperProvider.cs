﻿namespace Manager_Device_Service.Providers
{
    public static class AutoMapperProvider
    {
        public static IServiceCollection AddAutoMapperProvider(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            
            return services;
        }
    }
}
