using RavinduL.LocalNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace JsonApp
{
    public sealed partial class MainPage : Page
    {
        private readonly JsonApplication.App app;
        private LocalNotificationManager manager;
        public MainPage()
        {
            InitializeComponent();
            manager = new LocalNotificationManager(NotificationGrid);
            app = new JsonApplication.App(manager);
        }

        private void BtnFormat_Click(object sender, RoutedEventArgs e)
        {
            string currentText = TxtData.Text;
            TxtData.Text = app.FormatText(currentText);

        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            await app.SaveFileAsync(TxtData.Text);
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            string newText = await app.LoadFileAsync();
            if(newText is null)
            {
                return;
            }

            TxtData.Text = newText;
        }
    }
}