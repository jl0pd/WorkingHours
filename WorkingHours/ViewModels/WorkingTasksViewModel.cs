using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
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
        public ObservableCollection<WorkingTaskItemViewModel> Items { get; }

        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        : this (items.Select(t => new WorkingTaskItemViewModel(t)))
        {
        }

        public WorkingTasksViewModel(IEnumerable<WorkingTaskItemViewModel> viewModels)
        {
            Items = new ObservableCollection<WorkingTaskItemViewModel>(viewModels);
            Items.ToObservableChangeSet().OnItemAdded(vm =>
            {
                vm.OnCancelClick.Subscribe(async item =>
                {
                    IMsBoxWindow<ButtonResult> msgBox = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ContentMessage = $"Remove task '{item.Task.Name}'?",
                        ButtonDefinitions = ButtonEnum.YesNo
                    });

                    ButtonResult res = await (WindowingUtils.GetMainWindow() is Window parentWindow ? msgBox.ShowDialog(parentWindow) : msgBox.Show()).ConfigureAwait(true);

                    if (res == ButtonResult.Yes)
                    {
                        Items.Remove(item);
                    }
                });
            }).Subscribe();
        }
    }
}
