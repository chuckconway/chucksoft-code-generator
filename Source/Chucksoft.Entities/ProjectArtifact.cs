namespace Chucksoft.Entities
{
    public class ProjectArtifact
    {
        public ProjectArtifact()
        {
            
        }

        public ProjectArtifact(string name, bool createGeneratedFile)
        {
            Name = name;
            CreateGeneratedFile = createGeneratedFile;
        }

        public string Name { get; set; }
        public bool CreateGeneratedFile { get; set; }
    }
}
