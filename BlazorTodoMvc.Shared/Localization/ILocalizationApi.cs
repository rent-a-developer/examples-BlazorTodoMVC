using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents an API for localization related operations.
    /// </summary>
    public interface ILocalizationApi
    {
        /// <summary>
        /// Gets the supported cultures.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, which wraps the list of supported cultures.</returns>
        Task<List<Culture>> GetCultures();

        /// <summary>
        /// Gets the translations for the given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to get the translations for.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, which wraps the list of translations for the given culture.</returns>
        Task<List<Translation>> GetTranslations(String cultureName);
    }
}
