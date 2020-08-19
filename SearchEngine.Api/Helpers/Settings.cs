namespace SearchEngine.Api.Helpers
{
    public class AppSettings
    {
        public Settings Google { get; set; }
        public Settings Yandex { get; set; }
        public Settings Bing { get; set; }
    }

    public class Settings
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
    }
}