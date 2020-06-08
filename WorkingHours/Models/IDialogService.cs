using System;
using System.Threading.Tasks;

namespace WorkingHours.Models
{
    interface IDialogService
    {
        enum DialogResult
        {
            None,
            Ok,
            Yes,
            No,
            Abort,
            Cancel,
        }

        [Flags]
        enum ButtonType
        {
            None,
            Ok = 1,
            Yes = 2,
            No = 4,
            Cancel = 8,
            Abort = 16,

            OkCancel = Ok | Cancel,
            OkAbort = Ok | Abort,
            YesNo = Yes | No,
            YesNoCancel = Yes | No | Cancel,
            YesNoAbort = Yes | No | Abort,
        }

        Task<DialogResult> ShowMessageBox(string message, string title = "");

        Task<DialogResult> ShowDialog(string message, string title = "", ButtonType buttonType = ButtonType.Ok, object? window = null);
    }
}
