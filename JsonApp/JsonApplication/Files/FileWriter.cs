using JsonApp.JsonApplication.Files.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

namespace JsonApp.JsonApplication.Files
{
    class FileWriter
    {
        public const string OpertaionCancelled = "Operation cancelled";

        private readonly FileSavePicker fileSave;
        private readonly string defaultFileName;

        public FileWriter(FileDialogOptions fileDialogOptions)
        {
            fileSave = new FileSavePicker
            {
                SuggestedStartLocation = fileDialogOptions.StartLocation
            };
            foreach (var extension in fileDialogOptions.FileExtensions)
            {
                fileSave.FileTypeChoices.Add(extension.Name, new List<string> { extension.FileSuffix });
            }
            defaultFileName = fileDialogOptions.DefaultFileName;
            fileSave.SuggestedFileName = defaultFileName;
        }


        public async Task<SaveOperationResult> WriteToFile(string fileName, string content)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = defaultFileName;
            }
            fileSave.SuggestedFileName = fileName;

            return await SaveFile(content);
        }

        private async Task<SaveOperationResult> SaveFile(string content)
        {
            StorageFile file = await fileSave.PickSaveFileAsync();

            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, content);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    return new SaveOperationResult { IsSuccesful = true };
                }
                else
                {
                    return new SaveOperationResult { Message = $"Couldn't save file: {file.Name}" };
                }
            }
            else
            {
                return new SaveOperationResult { Message = OpertaionCancelled };
            }
        }
    }
}