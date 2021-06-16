using System;

namespace BlazorTodoMvc.Client.Model.Events
{
    /// <summary>
    /// Represents arguments for an event ocurring when the user wants to add a new todo item.
    /// </summary>
    public class AddTodoEventArgs
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="title">The title of the new todo item to add.</param>
        public AddTodoEventArgs(String title)
        {
            this.Title = title;
        }

        /// <summary>
        /// The title of the new todo item to add.
        /// </summary>
        public String Title { get; set; }
    }
}