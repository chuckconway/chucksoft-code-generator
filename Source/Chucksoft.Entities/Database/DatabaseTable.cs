using System.Collections.Generic;
using Chucksoft.Entities.Database;

namespace Chucksoft.Entities.Database
{
    public class DatabaseTable
    {
        public string Name { get; set; }
        public List<DatabaseColumn> Columns { get; set; }
    }
}