using System.Collections.Generic;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents data for localization.
    /// </summary>
    /// <param name="Cultures">A list of cultures supported by the application.</param>
    /// <param name="Translations">The translations of the application.</param>
    public record Localization(List<Culture> Cultures, List<Translation> Translations);
}
