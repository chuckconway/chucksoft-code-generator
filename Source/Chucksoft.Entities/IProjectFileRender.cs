using System.Collections.Generic;

namespace Chucksoft.Entities
{
    public interface IProjectFileRender
    {
        IVisualStudioProject Render<T>(CodeGenSettings settings, object template, IEnumerable<T> collection, IVisualStudioProject projectFile);
    }
}
