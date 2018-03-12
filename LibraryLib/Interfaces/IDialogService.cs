using Microsoft.Win32;
namespace LibraryLib
{
    public interface IDialogService
    {
        string FilePath { get; set; }

        void OpenFileDialog();
        string SaveFileDialog();
        OpenFileDialog OpenDialog { get; set; }
        SaveFileDialog SaveDialog { get; set; }
    }
}