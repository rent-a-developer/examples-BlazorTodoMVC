using System;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents a culture.
    /// </summary>
    /// <param name="Name">The name of the culture in the format languagecode-country/regioncode.</param>
    /// <param name="DisplayName">The full localized name of the culture.</param>
    /// <param name="TwoLetterISOLanguageName">The name of corresponding flag of the culture.</param>
    public record Culture(String Name, String DisplayName, String FlagName);
}
