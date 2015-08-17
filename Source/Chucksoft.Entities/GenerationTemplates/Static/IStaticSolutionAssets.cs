using System.Collections.Generic;

namespace Chucksoft.Entities.GenerationTemplates.Static
{
    public interface IStaticSolutionAsset : IFolder
    {
        List<GenerationArtifact> Render(CodeGenSettings settings);
    }
}