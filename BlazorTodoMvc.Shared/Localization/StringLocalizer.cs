using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents a service that provides localized strings.
    /// </summary>
    public class StringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="localizationProvider">The localization provider used to get translations.</param>
        public StringLocalizer(LocalizationProvider localizationProvider)
        {
            this.localizationProvider = localizationProvider;
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>The string resource as a <see cref="LocalizedString"/>.</returns>
        public LocalizedString this[string name] => this.localizationProvider.GetTranslation(name);

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        /// <returns>The formatted string resource as a <see cref="LocalizedString"/>.</returns>
        public LocalizedString this[string name, params object[] arguments] => this.localizationProvider.GetTranslation(name);

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">A <see cref="Boolean"/> indicating whether to include strings from parent cultures.</param>
        /// <returns>The strings.</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return this.localizationProvider.GetAllTranslations();
        }

        private readonly LocalizationProvider localizationProvider;
    }
}
