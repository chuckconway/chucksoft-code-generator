using System.Collections.Generic;

namespace Chucksoft.Entities.GenerationTemplates.Static
{
    /// <summary>
    /// Is rendered out once
    /// </summary>
    public interface IStaticAsset : IClassMetadata
    {
        List<StaticContent> Render();
    }
}