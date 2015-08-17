using Chucksoft.Entities.Database;

namespace Chucksoft.Entities.GenerationTemplates.Dynamic
{
    public interface IDynamicSolutionAsset : IFolder
    {
        string Filename { get; set;}
        GenerationArtifact RenderTemplate(DatabaseTable table, CodeGenSettings settings);
    }
}