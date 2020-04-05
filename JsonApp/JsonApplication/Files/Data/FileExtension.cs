namespace JsonApp.JsonApplication.Files.Data
{
    class FileExtension
    {
        private const string ExtensionStart = ".";

        private string fileSuffix;
        public string Name { get; set; }
        public string FileSuffix
        {
            get => fileSuffix;
            set => fileSuffix = value.StartsWith(ExtensionStart) ? value : $".{value}";
        }
    }
}