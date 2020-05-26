using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTasksViewModel : ViewModelBase
    {
        public ObservableCollection<WorkingTaskItemViewModel> Items { get; }

        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        {
            Items = new ObservableCollection<WorkingTaskItemViewModel>();

            Items.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                case NotifyCollectionChangedAction.Add:
                    foreach (WorkingTaskItemViewModel? item in e.NewItems)
                    {
                        item?.OnCancelClick.Subscribe(async t =>
                        {
                            Window? parentWindow = Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                                ? desktop.MainWindow
                                : null;

                            var msg = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
                            {
                                ContentMessage = "Remove task?",
                                ButtonDefinitions = ButtonEnum.YesNo
                            });

                            ButtonResult res = await (parentWindow != null
                                ? msg.ShowDialog(parentWindow)
                                : msg.Show());

                            if (res == ButtonResult.Yes)
                            {
                                Items.Remove(t);
                            }
                        });
                    }
                    break;
                }
            };

            Items.AddRange(items.Select(item => new WorkingTaskItemViewModel(item)));
        }
    }
}
