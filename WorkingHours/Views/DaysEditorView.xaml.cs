using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class DaysEditorView : UserControl
    {
        public DaysEditorView()
        {
            this.InitializeComponent();

            _carousel = this.FindControl<Carousel>(nameof(_carousel));
        }

        private readonly Carousel _carousel;

        private void OnLeftClick(object sender, RoutedEventArgs e)
        {
            _carousel.Previous();
        }

        private void OnRightClick(object sender, RoutedEventArgs e)
        {
            _carousel.Next();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
