using Jap.Services.EmailAPI.Messaging;

namespace Jap.Services.EmailAPI.Extension
{
    public static class AppBuilderExtensions
    {
        public static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder appBuilder)
        {
            ServiceBusConsumer = appBuilder.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostAppLife = appBuilder.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostAppLife.ApplicationStarted.Register(OnStart);
            hostAppLife.ApplicationStopping.Register(OnStop);

            return appBuilder;
        }

        private static void OnStart()
        {
            ServiceBusConsumer.Start();
        }

        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }
    }
}
