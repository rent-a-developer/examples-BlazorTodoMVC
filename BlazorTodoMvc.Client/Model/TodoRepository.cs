using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTodoMvc.Client.Model
{
    /// <summary>
    /// A repository for todo items.
    /// </summary>
    public class TodoRepository
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope factory. Mainly used to access the <see cref="ILocalStorageService"/> to store the todos.</param>
        public TodoRepository(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.Todos = ImmutableList<Todo>.Empty;
        }

        /// <summary>
        /// A list of all todo items.
        /// </summary>
        public ImmutableList<Todo> Todos { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// Loads the todo items from the local storage.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task Initialize()
        {
            var todos = await this.LoadTodos();
            this.Todos = ImmutableList<Todo>.Empty.AddRange(todos);
        }

        /// <summary>
        /// Adds a new todo item with the given title.
        /// </summary>
        /// <param name="title">The title to assign to the new todo item.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task AddTodo(String title)
        {
            this.Todos = this.Todos.Add(new Todo(title, false));
            await this.StoreTodos();
        }

        /// <summary>
        /// Changes the title of a todo item.
        /// </summary>
        /// <param name="todo">The todo item to change the title of.</param>
        /// <param name="newTitle">The new title to assign to the todo item.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task ChangeTodoTitle(Todo todo, String newTitle)
        {
            var newTodo = todo with { Title = newTitle };
            this.Todos = this.Todos.Replace(todo, newTodo);
            await this.StoreTodos();
        }

        /// <summary>
        /// Removes all completed todo items.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task RemoveCompletedTodos()
        {
            this.Todos = this.Todos.RemoveAll(a => a.IsComplete);
            await this.StoreTodos();
        }

        /// <summary>
        /// Removes the given todo item.
        /// </summary>
        /// <param name="todo">The todo item to remove.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task RemoveTodo(Todo todo)
        {
            this.Todos = this.Todos.Remove(todo);
            await this.StoreTodos();
        }

        /// <summary>
        /// Toggles the completion status of the given todo item.
        /// </summary>
        /// <param name="todo">The todo item to toggle the completion status of.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// If the given todo item is incomplete, this methods sets it to be completed.
        /// Otherwise this methods sets it to be incomplete.
        /// </remarks>
        public async Task ToggleTodo(Todo todo)
        {
            var newTodo = todo with { IsComplete = !todo.IsComplete };
            this.Todos = this.Todos.Replace(todo, newTodo);
            await this.StoreTodos();
        }

        /// <summary>
        /// Toggles the completion status of all todos.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// If all todos are currently completed, this methods sets all todo items to be incomplete.
        /// Otherwise this method sets all todo items to be complete.
        /// </remarks>
        public async Task ToggleAllTodos()
        {
            var areAllCompleted = this.Todos.All(a => a.IsComplete);

            if (areAllCompleted)
            {
                foreach (var todo in this.Todos)
                {
                    var newTodo = todo with { IsComplete = false };
                    this.Todos = this.Todos.Replace(todo, newTodo);
                }
            }
            else
            {
                foreach (var todo in this.Todos)
                {
                    var newTodo = todo with { IsComplete = true };
                    this.Todos = this.Todos.Replace(todo, newTodo);
                }
            }

            await this.StoreTodos();
        }

        private async Task<List<Todo>> LoadTodos()
        {
            using (var serviceScope = this.serviceScopeFactory.CreateScope())
            {
                var localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();
                var todos = await localStorage.GetItemAsync<List<Todo>>(TodosLocalStorageKey);

                if (todos == null)
                {
                    todos = this.CreateSampleTodos();
                    await localStorage.SetItemAsync(TodosLocalStorageKey, todos);
                }

                return todos;
            }
        }

        private async Task StoreTodos()
        {
            using (var serviceScope = this.serviceScopeFactory.CreateScope())
            {
                var localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();

                var todos = new List<Todo>(this.Todos);

                await localStorage.SetItemAsync(TodosLocalStorageKey, todos);
            }
        }

        private List<Todo> CreateSampleTodos()
        {
            return new List<Todo>
            {
                new Todo("Taste JavaScript", false),
                new Todo("Buy a unicorn", false)
            };
        }

        private readonly IServiceScopeFactory serviceScopeFactory;
        private const String TodosLocalStorageKey = "todos";
    }
}