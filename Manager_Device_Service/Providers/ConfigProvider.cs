namespace Manager_Device_Service.Providers
{
    public static class ConfigProvider
    {
        public static IServiceCollection LoadConfigs(this IServiceCollection services)
        {
            //var serviceProvider = services.BuildServiceProvider();
            //var configService = serviceProvider.GetRequiredService<ConfigService>();
            //configService.LoadConfigsAsync().Wait();

            return services;
        }
    }
}
