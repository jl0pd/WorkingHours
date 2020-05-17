using System;
using System.Collections.Generic;
using System.Text;
using Todo.Models;

namespace Todo.Services
{
    public class Database
    {
        public IEnumerable<TodoItem> GetItems() => new[]
        {
            new TodoItem("Buy milk"),
            new TodoItem("Walk dog"),
            new TodoItem("Learn Avalonia") { IsChecked = true }
        };
    }
}
