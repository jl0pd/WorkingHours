using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WorkingHours.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
    }

    public abstract class ViewModelBase<TModel> : ViewModelBase
    where TModel : class
    {
        [Reactive] public TModel? Model { get; set; }
    }
}
