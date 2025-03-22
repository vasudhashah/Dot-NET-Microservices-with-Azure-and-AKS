using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core
{
    //Extension method to add core services to the dependency injection container
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //To Do: Add services to the IOC container
            //core services often include data access, caching and other low-level components.
            return services;
        }
    }
}
