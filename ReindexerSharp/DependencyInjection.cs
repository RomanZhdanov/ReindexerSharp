using Microsoft.Extensions.DependencyInjection;

namespace ReindexerClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReindexer(this IServiceCollection services)
        {
            services.AddTransient<IReindexer, Reindexer>();

            return services;
        }
    }
}
