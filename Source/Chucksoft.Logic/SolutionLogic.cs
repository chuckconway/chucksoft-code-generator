using System.Collections.Generic;
using System.Reflection;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using System.IO;
using Chucksoft.Entities.GenerationTemplates.Static;

namespace Chucksoft.Logic
{
    public static class SolutionLogic
    {
        public static void Generate(CodeGenSettings settings, List<DatabaseTable> tables)
        {
            //generate project files (classes, static assets)
            List<IVisualStudioProject> projects = GenerateClasses(settings, tables);

            //Add projects to the solution
            VisualStudioSolution solution = new VisualStudioSolution {Projects = projects};
            solution.Render(settings.CodeGenerationDirectory, settings.SolutionNamespace);

            //generate solution assets (Database scripts, thirdparty assemblies)...
            GenerateStaticSolutionAssets(settings);
            GenerateDynamicSolutionAssets(settings, tables);
        }

        private static void GenerateStaticSolutionAssets(CodeGenSettings settings)
        {
            List<IStaticSolutionAsset> dynamicTemplates = RetrieveTemplates<IStaticSolutionAsset>(settings, "IStaticSolutionAsset");

            foreach (IStaticSolutionAsset asset in dynamicTemplates)
            {
                List<GenerationArtifact> assets = asset.Render(settings);

                foreach (GenerationArtifact sa in assets)
                {
                    string content = sa.Content.ToString();
                    string dirPath = string.Format("{0}\\{1}", settings.CodeGenerationDirectory, asset.FolderName);
                    string fullFilePath = string.Format("{0}\\{1}", dirPath, sa.FileName);

                    if (!File.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    File.WriteAllText(fullFilePath, content);
                }
            }
        }

        private static void GenerateDynamicSolutionAssets(CodeGenSettings settings, IEnumerable<DatabaseTable> tables)
        {
            List<IDynamicSolutionAsset> dynamicTemplates = RetrieveTemplates<IDynamicSolutionAsset>(settings,"IDynamicSolutionAsset");
 
            foreach (IDynamicSolutionAsset asset in dynamicTemplates)
            {
                foreach (DatabaseTable table in tables)
                {
                    GenerationArtifact content = asset.RenderTemplate(table, settings);
                    string dirPath = string.Format("{0}\\{1}", settings.CodeGenerationDirectory, asset.FolderName);
                    string fullFilePath = string.Format("{0}\\{1}", dirPath, content.FileName);

                    if(!File.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    File.WriteAllText(fullFilePath, content.Content.ToString());
                }
            }
        }

        private static List<T> RetrieveTemplates<T>(CodeGenSettings settings, string interfaceName)
        {
            List<Assembly> assemblies = ReflectionHelper<T>.LoadAssemblies(settings.CompiledTemplateLocation);
            List<T> templates = ReflectionHelper<T>.LoadTemplates(assemblies, interfaceName);

            return templates;
        }

        private static List<IVisualStudioProject> GenerateClasses(CodeGenSettings settings, List<DatabaseTable> tables)
        {
            List<IVisualStudioProject> vsProjects = new List<IVisualStudioProject>();
            vsProjects = ClassesLogic.GenerateDynamicClasses(settings, tables, vsProjects);
            vsProjects = ClassesLogic.GenerateStaticClasses(settings, vsProjects);

            //Renders projects and the added files.
            ClassesLogic.RenderProjects(vsProjects);

            return vsProjects;
        }
    }
}
