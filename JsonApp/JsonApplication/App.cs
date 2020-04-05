using JsonApp.JsonApplication.Files;
using JsonApp.JsonApplication.Files.Data;
using JsonApp.JsonApplication.Formatter;
using RavinduL.LocalNotifications;
using RavinduL.LocalNotifications.Notifications;
using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace JsonApp.JsonApplication
{
    public class App
    {
        private const int ShortNotificationVisibilityTime = 3;
        private const int LongNotificationVisibilityTime = 10;
        private readonly JsonFormatter formatter;
        private readonly FileReader reader;
        private readonly FileWriter writer;
        private string fileName;
        private readonly LocalNotificationManager manager;

        public App(LocalNotificationManager manager)
        {
            this.manager = manager;
            formatter = new JsonFormatter();
            FileDialogOptions readOptions = new FileDialogOptions
            {
                StartLocation = PickerLocationId.DocumentsLibrary,
                FileExtensions = new[]
                {
                    new FileExtension {Name = "JSON", FileSuffix = "json"},
                    new FileExtension {Name = "TEXT", FileSuffix = "txt"}
                }
            };

            FileDialogOptions writeOptions = new FileDialogOptions
            {
                StartLocation = PickerLocationId.DocumentsLibrary,
                DefaultFileName = "formattedJson",
                FileExtensions = new[]
                {
                    new FileExtension {Name = "JSON", FileSuffix = "json"},
                }
            };

            reader = new FileReader(readOptions);
            writer = new FileWriter(writeOptions);
        }

        public string FormatText(string textToFormat)
        {
            if (string.IsNullOrWhiteSpace(textToFormat))
            {
                ShowNotification("Cannot format empty text", Colors.Orange, TimeSpan.FromSeconds(ShortNotificationVisibilityTime));

                return textToFormat;
            }
            var result = formatter.Format(textToFormat);

            if (string.IsNullOrEmpty(result.Error))
            {
                return result.FormattedText;
            }

            ShowNotification(result.Error, Colors.Red, TimeSpan.FromSeconds(LongNotificationVisibilityTime));

            return textToFormat;
        }

        public async Task<string> LoadFileAsync()
        {
            string content = await reader.ReadFile();
            fileName = reader.File?.Name;

            return content;
        }

        public async Task SaveFileAsync(string content)
        {
            var result = await writer.WriteToFile(fileName, content);
            if (!result.IsSuccesful && result.Message != FileWriter.OpertaionCancelled)
            {
                ShowNotification(result.Message);
            }
        }

        private void ShowNotification(string content, Color? backgroundColor = null, TimeSpan? visibilityTime = null)
        {
            manager.Show(new SimpleNotification
            {
                TimeSpan = visibilityTime,
                Text = content,
                Background = new SolidColorBrush
                {
                    Color = backgroundColor ?? Colors.Gray
                }
            });
        }
    }
}