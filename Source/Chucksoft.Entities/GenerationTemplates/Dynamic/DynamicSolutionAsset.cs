using Chucksoft.Entities.Database;

namespace Chucksoft.Entities.GenerationTemplates.Dynamic
{
    public abstract class DynamicSolutionAsset : IDynamicSolutionAsset
    {
        public string Filename { get; set;}
        public abstract GenerationArtifact RenderTemplate(DatabaseTable table, CodeGenSettings settings);
        
        public string FolderName { get; set;}
    }
}