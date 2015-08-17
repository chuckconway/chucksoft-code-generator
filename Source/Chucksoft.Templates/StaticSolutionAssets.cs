using System.Collections.Generic;
using Chucksoft.Entities;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Static;

namespace Chucksoft.Templates
{
    public class StaticSolutionAssets : StaticSolutionAsset
    {
        public StaticSolutionAssets()
        {
            FolderName = @"Dump\";
        }

        public override List<GenerationArtifact> Render(CodeGenSettings settings)
        {
            List<GenerationArtifact> artifact = new List<GenerationArtifact>();
            return artifact;
        }
    }
}
