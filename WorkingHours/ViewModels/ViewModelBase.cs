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
        public ViewModelBase(TModel? model) => Model = model;

        public ViewModelBase() { }

        [Reactive] public virtual TModel? Model { get; set; }
    }
}
