﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Content = List = new WorkingTasksViewModel(new[]
            {
                new WorkingTask("1243"),
                new WorkingTask("1245"),
                new WorkingTask("1246"),
            });
        }
        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public void AddItem()
        {
            var vm = new AddTaskViewModel();

            Observable.Merge(vm.Add, vm.Cancel.Select(_ => (WorkingTask?)null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        List.Items.Add(new WorkingTaskItemViewModel(item));
                    }
                    Content = List;
                });
            Content = vm;
        }

        private ViewModelBase _content = null!;

        public WorkingTasksViewModel List { get; }
    }
}