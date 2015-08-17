using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.GenerationTemplates.Dynamic;

namespace Chucksoft.Templates
{
    public class Logic : CompiledTemplate
    {
        public Logic()
        {
            ProjectNamespace = "Logic";
            ClassFilePath = string.Empty;
            FilenameAppending = "Logic";
        }

        public override string RenderTemplate(DatabaseTable table, CodeGenSettings settings)
        {
            return "ChuckWasHere";
        }
    }
}
