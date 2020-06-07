using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using WorkingHours.Utils;
using WorkingHours.ViewModels;

namespace WorkingHours
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            data = data.ThrowIfNull(nameof(data));

            IControl? ret = null;
            
            string? name = data.GetType().FullName?.Replace("ViewModel", "View", StringComparison.InvariantCulture);
            if (name != null)
            {
                var type = Type.GetType(name);
                if (type != null)
                {
                    ret = (IControl?)Activator.CreateInstance(type);
                }
            }

            return ret ?? new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data) => data is ViewModelBase;
    }
}