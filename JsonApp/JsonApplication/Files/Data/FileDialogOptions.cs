using Windows.Storage.Pickers;

namespace JsonApp.JsonApplication.Files.Data
{
    class FileDialogOptions
    {
        public FileExtension[] FileExtensions { get; set; }
        public string DefaultFileName { get; set; }
        public PickerLocationId StartLocation { get; set; }
    }
}