using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows;
using MessageBox.Avalonia.Enums;
using WorkingHours.Models;

namespace WorkingHours.Providers
{
    class AvaloniaDialogService : IDialogService
    {
        public async Task<IDialogService.DialogResult> ShowDialog(string message, string title = "",
            IDialogService.ButtonType buttonType = IDialogService.ButtonType.Ok, object? window = null)
        {
            IMsBoxWindow<ButtonResult> msgBox = MessageBoxManager.GetMessageBoxStandardWindow(title, message, Convert(buttonType));

            ButtonResult res = await (window is Window parentWindow ? msgBox.ShowDialog(parentWindow) : msgBox.Show()).ConfigureAwait(true);

            return Convert(res);
        }

        public Task<IDialogService.DialogResult> ShowMessageBox(string message, string title = "") 
            => ShowDialog(message, title);

        IDialogService.ButtonType Convert(ButtonEnum button) => button switch
        {
            ButtonEnum.Ok => IDialogService.ButtonType.Ok,
            ButtonEnum.YesNo => IDialogService.ButtonType.YesNo,
            ButtonEnum.OkCancel => IDialogService.ButtonType.OkCancel,
            ButtonEnum.OkAbort => IDialogService.ButtonType.OkAbort,
            ButtonEnum.YesNoCancel => IDialogService.ButtonType.YesNoCancel,
            ButtonEnum.YesNoAbort => IDialogService.ButtonType.YesNoAbort,
            _ => throw new NotImplementedException(),
        };

        IDialogService.DialogResult Convert(ButtonResult button) => button switch
        {
            ButtonResult.Ok => IDialogService.DialogResult.Ok,
            ButtonResult.Yes => IDialogService.DialogResult.Yes,
            ButtonResult.No => IDialogService.DialogResult.No,
            ButtonResult.Abort => IDialogService.DialogResult.Abort,
            ButtonResult.Cancel => IDialogService.DialogResult.Cancel,
            ButtonResult.None => IDialogService.DialogResult.None,
            _ => throw new NotImplementedException(),
        };

        ButtonEnum Convert(IDialogService.ButtonType button) => button switch
        {
            IDialogService.ButtonType.Ok => ButtonEnum.Ok,
            IDialogService.ButtonType.OkCancel => ButtonEnum.OkCancel,
            IDialogService.ButtonType.OkAbort => ButtonEnum.OkAbort,
            IDialogService.ButtonType.YesNo => ButtonEnum.YesNo,
            IDialogService.ButtonType.YesNoCancel => ButtonEnum.YesNoCancel,
            IDialogService.ButtonType.YesNoAbort => ButtonEnum.YesNoAbort,
            _ => throw new NotImplementedException(),
        };

        ButtonResult Convert(IDialogService.DialogResult result) => result switch
        {
            IDialogService.DialogResult.None => ButtonResult.None,
            IDialogService.DialogResult.Ok => ButtonResult.Ok,
            IDialogService.DialogResult.Yes => ButtonResult.Yes,
            IDialogService.DialogResult.No => ButtonResult.No,
            IDialogService.DialogResult.Abort => ButtonResult.Abort,
            IDialogService.DialogResult.Cancel => ButtonResult.Cancel,
            _ => throw new NotImplementedException(),
        };
    }
}
