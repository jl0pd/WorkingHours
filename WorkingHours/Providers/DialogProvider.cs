using System.Threading.Tasks;
using WorkingHours.Models;

namespace WorkingHours.Providers
{
    static class DialogProvider
    {
        static public IDialogService DialogService { get; set; } = null!;

        static public Task<IDialogService.DialogResult> ShowDialog(string message, string title = "",
            IDialogService.ButtonType buttonType = IDialogService.ButtonType.Ok, object? window = null)
            => DialogService.ShowDialog(message, title, buttonType, window);

        static public Task<IDialogService.DialogResult> ShowMessageBox(string message, string title = "")
            => DialogService.ShowMessageBox(message, title);
    }
}
