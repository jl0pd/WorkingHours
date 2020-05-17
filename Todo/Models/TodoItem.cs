using ReactiveUI;

namespace Todo.Models
{
    public class TodoItem : ReactiveObject
    {
        private bool _isChecked;

        public TodoItem(string? description)
        {
            Description = description;
        }

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }

        public string? Description { get; set; }
    }
}
