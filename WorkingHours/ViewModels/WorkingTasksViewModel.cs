using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using WorkingHours.Models;
using WorkingHours.Utils;

namespace WorkingHours.ViewModels
{
    public class WorkingTasksViewModel : ViewModelBase
    {
        public SourceList<WorkingTaskItemViewModel> WorkingTaskItemViewModels { get; } = new SourceList<WorkingTaskItemViewModel>();

        private IEnumerable<WorkingTaskItemViewModel> Items => WorkingTaskItemViewModels.Items;

        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        {
            WorkingTaskItemViewModels.Connect().OnItemAdded(vm => vm.OnCancelClick.Subscribe(async item =>
            {
                IMsBoxWindow<ButtonResult> msgBox = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ContentMessage = $"Remove task '{item.Task.Name}'?",
                    ButtonDefinitions = ButtonEnum.YesNo
                });

                ButtonResult res = await (WindowingUtils.GetMainWindow() is Window parentWindow ? msgBox.ShowDialog(parentWindow) : msgBox.Show()).ConfigureAwait(false);

                if (res == ButtonResult.Yes)
                {
                    WorkingTaskItemViewModels.Remove(item);
                    this.RaisePropertyChanged(nameof(Items));
                }
            })).Subscribe();

            WorkingTaskItemViewModels.AddRange(items.Select(item => new WorkingTaskItemViewModel(item)));
        }
    }
}
