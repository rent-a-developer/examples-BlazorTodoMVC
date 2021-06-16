namespace BlazorTodoMvc.Client.Model.Events
{
    /// <summary>
    /// Represents arguments for an event relating a todo item.
    /// </summary>
    public class TodoEventArgs
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="todo">The todo item the event is about.</param>
        public TodoEventArgs(Todo todo)
        {
            this.Todo = todo;
        }

        /// <summary>
        /// The todo item the event is about.
        /// </summary>
        public Todo Todo { get; set; }
    }
}