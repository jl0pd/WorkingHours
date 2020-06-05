using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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

        public MiniMainWindow(WorkingTaskItemViewModel workingTask)
        : this()
        {
            DockPanel a = this.FindControl<DockPanel>("DockPanel");
            IControl view = new ViewLocator().Build(workingTask);
            view.DataContext = workingTask;

            Observable
                .Merge(workingTask.OnCancelClick, workingTask.OnStopClick)
                .ForEachAsync(_ => Close());
            a.Children.Add(view);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (IsPointerPressed && IsPointerOver)
            {
                Position = ToPixelPoint(GetAbsoluteCoords(e)) - PressPoint;
            }
            base.OnPointerMoved(e);
        }

        private static PixelPoint ToPixelPoint(Point p) => new PixelPoint((int)p.X, (int)p.Y);

        private Point GetAbsoluteCoords(PointerEventArgs e)
        {
            Point pos = e.GetPosition(this);
            return new Point(Position.X + pos.X, Position.Y + pos.Y);
        }

        private bool IsPointerPressed { get; set; }

        private PixelPoint PressPoint { get; set; }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            PressPoint = ToPixelPoint(e.GetPosition(this));
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
