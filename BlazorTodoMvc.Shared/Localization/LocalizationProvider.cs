using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Provides localization related data and operations.
    /// </summary>
    public class LocalizationProvider
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope factory. Mainly used to access the <see cref="ILocalStorageService"/>.</param>
        public LocalizationProvider(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// The list of cultures supported by the application.
        /// </summary>
        public IEnumerable<Culture> Cultures { get; private set; }
        
        /// <summary>
        /// Gets the culture currently in use.
        /// </summary>
        public Culture CurrentCulture => this.GetCurrentCulture();

        /// <summary>
        /// Initializes this instance.
        /// Loads the supported cultures and the translations via the given localization API.
        /// </summary>
        /// <param name="localizationApi">The localization API to be used to load cultures and translations.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task Initialize(ILocalizationApi localizationApi)
        {
            this.Cultures = await localizationApi.GetCultures();

            String currentCultureName;

            using (var serviceScope = this.serviceScopeFactory.CreateScope())
            {
                var localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();

                currentCultureName = await localStorage.GetItemAsync<String>(CurrentCultureLocalStorageKey);

                if (currentCultureName == null)
                {
                    currentCultureName = this.Cultures.First().Name;
                    await localStorage.SetItemAsync(CurrentCultureLocalStorageKey, currentCultureName);
                }

                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(currentCultureName);
            }

            var translations = await localizationApi.GetTranslations(currentCultureName);
            this.translations = translations.ToDictionary(a => a.Term, a => a.Value);
        }

        /// <summary>
        /// Gets the translation of the given term.
        /// </summary>
        /// <param name="term">The term to get the translation for.</param>
        /// <returns>An instance of <see cref="LocalizedString"/> containing the translation of the given term.</returns>
        public LocalizedString GetTranslation(String term)
        {
            if (!this.translations.TryGetValue(term, out String translationValue))
            {
                return new LocalizedString(term, $"Translation missing for '{term}'", true, nameof(LocalizationProvider));
            }

            return new LocalizedString(term, translationValue, false, nameof(LocalizationProvider));
        }

        /// <summary>
        /// Gets a list of all available translations.
        /// </summary>
        /// <returns>A list of all available translations.</returns>
        public List<LocalizedString> GetAllTranslations()
        {
            return this.translations.Select(a => new LocalizedString(a.Key, a.Value, false, nameof(LocalizationProvider))).ToList();
        }

        /// <summary>
        /// Changes the current culture of the application.
        /// </summary>
        /// <param name="cultureName">The name of the culture to change to.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task ChangeCulture(String cultureName)
        {
            using (var serviceScope = this.serviceScopeFactory.CreateScope())
            {
                var localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();
                await localStorage.SetItemAsync(CurrentCultureLocalStorageKey, cultureName);
            }
        }

        private Culture GetCurrentCulture()
        {
            var currentCultureName = CultureInfo.CurrentCulture.Name;

            var culture = this.Cultures.FirstOrDefault(a => a.Name == currentCultureName);

            if (culture == null)
            {
                throw new Exception($"The culture '{currentCultureName}' is not supported.");
            }

            return culture;
        }

        private readonly IServiceScopeFactory serviceScopeFactory;
        private Dictionary<String, String> translations;

        private const String CurrentCultureLocalStorageKey = "culture";
    }
}
