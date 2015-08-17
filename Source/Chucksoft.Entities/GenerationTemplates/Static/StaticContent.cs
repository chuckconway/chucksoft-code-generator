namespace Chucksoft.Entities.GenerationTemplates.Static
{
    public class StaticContent
    {
        public string FileName { get; set;}
        public bool SetNamespace { get; set; }
        public bool CreateGeneratedCounterpart { get; set; }

        public object Content { get; set;}

    }
}