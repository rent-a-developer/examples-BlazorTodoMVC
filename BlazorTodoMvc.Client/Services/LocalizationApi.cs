using BlazorTodoMvc.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorTodoMvc.Client.Services
{
    /// <summary>
    /// An API for localization related operations.
    /// </summary>
    public class LocalizationApi : ILocalizationApi
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> used to send requests to the backend.</param>
        public LocalizationApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets the supported cultures.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, which wraps the list of supported cultures.</returns>
        public async Task<List<Culture>> GetCultures()
        {
            return await this.httpClient.GetFromJsonAsync<List<Culture>>("api/Localization/Cultures");
        }

        /// <summary>
        /// Gets the translations for the given culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture to get the translations for.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation, which wraps the list of translations for the given culture.</returns>
        public async Task<List<Translation>> GetTranslations(String cultureName)
        {
            return await this.httpClient.GetFromJsonAsync<List<Translation>>("api/Localization/Translations/" + cultureName);
        }

        private readonly HttpClient httpClient;
    }
}
