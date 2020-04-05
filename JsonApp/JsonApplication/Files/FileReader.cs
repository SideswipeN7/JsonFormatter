using JsonApp.JsonApplication.Files.Data;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace JsonApp.JsonApplication.Files
{
    class FileReader
    {
        private readonly FileOpenPicker fileOpen;
        public StorageFile File { get; set; }
        public FileReader(FileDialogOptions fileDialogOptions)
        {
            fileOpen = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = fileDialogOptions.StartLocation
            };
            foreach (var extension in fileDialogOptions.FileExtensions)
            {
                fileOpen.FileTypeFilter.Add(extension.FileSuffix);
            }
        }

        public async Task<string> ReadFile()
        {
            File = await fileOpen.PickSingleFileAsync();
            if (File is null)
            {
                return null;
            }

            return await FileIO.ReadTextAsync(File);
        }
    }
}