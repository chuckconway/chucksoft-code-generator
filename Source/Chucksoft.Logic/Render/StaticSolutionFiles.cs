using System.Collections.Generic;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;

namespace Chucksoft.Logic.Render
{
    class StaticSolutionFiles : ISolutionFileRender
    {

        #region ISolutionFileRender Members

        public void Render(CodeGenSettings settings, object template, IEnumerable<DatabaseTable> collection)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
