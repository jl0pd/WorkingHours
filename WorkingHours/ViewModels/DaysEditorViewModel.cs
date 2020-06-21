using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class DaysEditorViewModel : ViewModelBase<ObservableCollection<WorkingDayViewModel>>
    {
        public ObservableCollection<WorkingDayViewModel>? DaysViewModels => Model;

        public IEnumerable<WorkingDay>? Days
            => Model?.Where(m => !(m.Model is null)).Select(d => d.Model!);

        [Reactive] public WorkingDayViewModel? SelectedDay { get; set; }

        public DaysEditorViewModel(IEnumerable<WorkingDay>? days)
        : base(new ObservableCollection<WorkingDayViewModel>(days?.Select(d => new WorkingDayViewModel(d))))
        {
            var currentDay = Model?.FirstOrDefault(d => d.Date == DateTime.Today);
            currentDay ??= Model?.FirstOrDefault();
            if (!(currentDay is null))
            {
                SelectedDay = currentDay;
            }
        }

        public DaysEditorViewModel()
        : this(null)
        {
        }

        //public CurrentDayEditorViewModel(IEnumerable<WorkingTaskViewModel> viewModels)
        //{
        //    Items = new ObservableCollection<WorkingTaskViewModel>(viewModels);
        //    Items.ToObservableChangeSet().OnItemAdded(vm =>
        //    {
        //        vm.OnCancelClick.Subscribe(async item =>
        //        {
        //            DialogResult res = await DialogProvider.ShowDialog($"Remove task '{item.Task.Name}'?", "Remove", 
        //                ButtonType.YesNo, WindowingUtils.GetMainWindow()).ConfigureAwait(true);

        //            if (res == DialogResult.Yes)
        //            {
        //                Items.Remove(item);
        //            }
        //        });
        //    }).Subscribe();
        //}
    }
}
