namespace Space.Server.AI.Core.Settings
{
    public sealed class OllamaSettings
    {
        public string ModelName { get; set; }
        public string Host { get; set; }
        public int Temperature { get; set; } = 0;
    }
}
