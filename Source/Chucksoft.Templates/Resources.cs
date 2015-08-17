using System.Text;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using Chucksoft.Resources.Data;

namespace Chucksoft.Templates
{
    public class Resources : CompiledTemplate
    {
        public Resources()
        {
            ProjectNamespace = "Repository";
            ClassFilePath = @"Infrastructure\Repositories\";
            FilenameAppending = "Repository";
        }

        /// <summary>
        /// Renders the template.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public override string RenderTemplate(DatabaseTable table, CodeGenSettings settings)
        {
            IGenerateMethods generateMethods = new DynamicDbParametersResources();

            if(!settings.UseDynamicParameters)
            {
                generateMethods = new StaticDbParametersResources();
            }

            return generateMethods.Render(table, settings);
        }
    }
}
