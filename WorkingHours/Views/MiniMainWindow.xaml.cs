using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WorkingHours.Extensions;
using WorkingHours.Utils;
using WorkingHours.ViewModels;

namespace WorkingHours.Views
{
    public class MiniMainWindow : Window
    {
        public MiniMainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnCloseClick(object sender, RoutedEventArgs e) => Close();

        internal MiniMainWindow(WorkingTaskItemViewModel workingTask)
        : this()
        {
            DockPanel a = this.FindControl<DockPanel>("DockPanel");
            IControl view = new ViewLocator().Build(workingTask);
            view.DataContext = workingTask;

            Observable
                .Merge(workingTask.OnCancelClick, workingTask.OnStopClick)
                .Take(1)
                .Subscribe(_ => Close());
            a.Children.Add(view);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            e = e.ThrowIfNull(nameof(e));

            if (IsPointerPressed && IsPointerOver)
            {
                Position = GetAbsoluteCoords(e).ToPixelPoint() - PressPoint;
            }
            base.OnPointerMoved(e);
        }

        private Point GetAbsoluteCoords(PointerEventArgs e)
            => Position.ToPoint() + e.GetPosition(this);

        private bool IsPointerPressed { get; set; }

        private PixelPoint PressPoint { get; set; }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            e = e.ThrowIfNull(nameof(e));

            PressPoint = e.GetPosition(this).ToPixelPoint();
            IsPointerPressed = true;
            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            IsPointerPressed = false;
            base.OnPointerReleased(e);
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
