using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using WorkingHours.Models;

namespace WorkingHours.Converters
{
    public class TaskStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const string startedGreen = "#22572D";
            const string completedBlue = "#284B66";
            const string canceledRed = "#7A0000";
            const string notStartedGray = "#2E2E2E";

            Color color = (WorkingTask.State)value switch
            {
                WorkingTask.State.NotStarted => Color.Parse(notStartedGray),
                WorkingTask.State.Started => Color.Parse(startedGreen),
                WorkingTask.State.Completed => Color.Parse(completedBlue),
                WorkingTask.State.Canceled => Color.Parse(canceledRed),
                _ => throw new NotImplementedException(),
            };
            
            if (targetType == typeof(Color))
            {
                return color;
            }
            else if (targetType == typeof(IBrush))
            {
                return new SolidColorBrush(color);
            }

            return BindingOperations.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
