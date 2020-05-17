using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Todo.ViewModels;

namespace Todo
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            IControl? ret = null;
            string? name = data.GetType().FullName?.Replace("ViewModel", "View");
            if (name != null)
            {
                var type = Type.GetType(name);
                if (type != null)
                {
                    ret = (IControl?)Activator.CreateInstance(type);
                }
            }

            return ret ?? new TextBlock
            {
                Text = $"Not Found: {name}",
                Background = Brushes.Red
            };
        }

        public bool Match(object data) => data is ViewModelBase;
    }
}