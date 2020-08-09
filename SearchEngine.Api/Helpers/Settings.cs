namespace SearchEngine.Api.Helpers
{
    public class AppSettings
    {
        public GoogleSettings Google { get; set; }
        public YandexleSettings Yandex { get; set; }
        public BingSettings Bing { get; set; }
    }

    public class Settings
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
    }

    public class GoogleSettings : Settings
    {

    }

    public class YandexleSettings : Settings
    {

    }

    public class BingSettings : Settings
    {

    }
}