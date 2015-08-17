using System.Collections.Generic;
using System.Text;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates.Dynamic;

namespace Chucksoft.Templates
{
    public class Model : CompiledTemplate
    {
        public Model()
        {
            ProjectNamespace = "Model";
            ClassFilePath = @"Domain\Model\";
        }

        public override string RenderTemplate(DatabaseTable table, CodeGenSettings settings)
        {
            string contents = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(Model), "Chucksoft.Templates.Templates.Enity.template");

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(contents))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Templates.Templates.Enity.template\"");
            }
            
            contents = contents.Replace("<%[Properties]%>", RenderProperties(table.Columns));
            return contents;
        }

        public string RenderProperties(List<DatabaseColumn> columns)
        {
            StringBuilder builder = new StringBuilder();

            foreach (DatabaseColumn column in columns)
            {
#pragma warning disable 162
                builder.AppendLine(string.Format("\t\tpublic {0}{1} {2} {{ get; set; }}", column.ColumnType, (false /*column.IsNullable */ ? "?" : ""), column.Name));
#pragma warning restore 162
            }

            return builder.ToString();
        }

    }
}
