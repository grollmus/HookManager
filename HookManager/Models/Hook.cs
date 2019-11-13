namespace HookManager.Models
{
    internal class Hook
    {
        public string Description { get; set; }
        public string ScriptFileName { get; set; }
        public string ScriptFilePath { get; set; }
        public string Title { get; set; }
        public HookType Type { get; set; }
    }
}