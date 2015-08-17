using System.Collections.Generic;
using Chucksoft.Entities.GenerationTemplates.Static;

namespace Chucksoft.Entities.GenerationTemplates.Static
{
    public abstract class StaticSolutionAsset : IStaticSolutionAsset
    {
        public abstract List<GenerationArtifact> Render(CodeGenSettings settings);
        public string FolderName { get; set; }

    }
}