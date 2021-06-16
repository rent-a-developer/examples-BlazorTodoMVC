using System;

namespace BlazorTodoMvc.Client.Model.Events
{
    /// <summary>
    /// Represents arguments for an event ocurring when the user wants to change the title of a todo item.
    /// </summary>
    public class ChangeTodoTitleEventArgs : TodoEventArgs
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="todo">The todo item to change the title of.</param>
        /// <param name="newTitle">The new title to assign to the todo item.</param>
        public ChangeTodoTitleEventArgs(Todo todo, String newTitle) : base(todo)
        {
            this.NewTitle = newTitle;
        }

        /// <summary>
        /// The new title to assign to the todo item.
        /// </summary>
        public String NewTitle { get; set; }
    }
}
