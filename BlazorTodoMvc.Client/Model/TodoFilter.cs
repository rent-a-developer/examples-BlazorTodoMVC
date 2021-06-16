namespace BlazorTodoMvc.Client.Model
{
    /// <summary>
    /// Defines filters for todo items.
    /// </summary>
    public enum TodoFilter
    {
        /// <summary>
        /// Show all todo items.
        /// </summary>
        All,

        /// <summary>
        /// Show only active (not yet completed) todo items.
        /// </summary>
        Active,

        /// <summary>
        /// Show only completed todo items.
        /// </summary>
        Completed
    }
}
