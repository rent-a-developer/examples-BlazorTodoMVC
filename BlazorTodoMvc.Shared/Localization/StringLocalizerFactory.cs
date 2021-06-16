using Microsoft.Extensions.Localization;
using System;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents a factory that creates <see cref="IStringLocalizer"/> instances.
    /// </summary>
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="localizationProvider">The localization provider used to get translations.</param>
        public StringLocalizerFactory(LocalizationProvider localizationProvider)
        {
            this.localizationProvider = localizationProvider;
        }

        /// <summary>
        /// Creates an <see cref="IStringLocalizer"/> using the <see cref="Assembly"/> and <see cref="Type.FullName"/> of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="resourceSource">The <see cref="Type"/>.</param>
        /// <returns>The <see cref="IStringLocalizer"/>.</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            return new StringLocalizer(this.localizationProvider);
        }

        /// <summary>
        /// Creates an <see cref="IStringLocalizer"/>.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>The <see cref="IStringLocalizer"/>.</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer(this.localizationProvider);
        }

        private readonly LocalizationProvider localizationProvider;
    }
}
