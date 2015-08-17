using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;

namespace Chucksoft.Templates
{
    public interface IGenerateMethods
    {
        string Render(DatabaseTable table, CodeGenSettings settings);
    }
}
