using System;

namespace BlazorTodoMvc.Client.Model
{
    /// <summary>
    /// Represents a todo item.
    /// </summary>
    /// <param name="Title">The title of the todo item.</param>
    /// <param name="IsComplete">Determines if the todo item has been completed.</param>
    public record Todo(String Title, Boolean IsComplete);
}
