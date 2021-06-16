using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Provides localization related extension methods for the <see cref="IServiceCollection"/> type.
    /// </summary>
    public static class LocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the localization related services to the given list of services.
        /// </summary>
        /// <param name="services">The list of services to add the localization services to.</param>
        /// <returns>The given list of services.</returns>
        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddSingleton<LocalizationProvider>();
            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer), typeof(StringLocalizer));

            return services;
        }
    }
}
