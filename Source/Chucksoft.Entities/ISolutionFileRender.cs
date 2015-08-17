using System.Collections.Generic;
using Chucksoft.Entities.Database;

namespace Chucksoft.Entities
{
    public interface ISolutionFileRender
    {
        void Render(CodeGenSettings settings, object template, IEnumerable<DatabaseTable> collection);
    }
}
