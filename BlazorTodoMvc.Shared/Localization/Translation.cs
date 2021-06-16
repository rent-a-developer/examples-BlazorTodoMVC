using System;

namespace BlazorTodoMvc.Shared.Localization
{
    /// <summary>
    /// Represents a translation of a term.
    /// </summary>
    /// <param name="Term">The term that is translated.</param>
    /// <param name="Culture">The name of the culture of the translation.</param>
    /// <param name="Value">The translation.</param>
    public record Translation(String Term, String Culture, String Value);
}
