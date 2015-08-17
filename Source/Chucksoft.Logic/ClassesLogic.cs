using System.Collections.Generic;
using System.Reflection;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Dynamic;
using Chucksoft.Entities.GenerationTemplates.Static;
using Chucksoft.Logic.Projects;
using Chucksoft.Logic.Render;

namespace Chucksoft.Logic
{
    public class ClassesLogic
    {

        /// <summary>
        /// Renders static content ie. base classes, CSS Files. These files don't require the database schemea.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="projects">A collection of projects, the Dynamic Classes will be added to this collection</param>
        /// <returns>A collection of projects with rendered content added</returns>
        public static List<IVisualStudioProject> GenerateStaticClasses(CodeGenSettings settings,List<IVisualStudioProject> projects)
        {
            IProjectFileRender fileRender = new StaticProjectFiles();
            projects = Generate<IStaticAsset, string>("IStaticAsset", settings, projects, fileRender, null);
            return projects;
        }

        /// <summary>
        /// Renders templates based on the database schema
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="tables">A collection of tables and columns used to generate code</param>
        /// <param name="projects">A collection of projects, the Dynamic Classes will be added to this collection</param>
        /// <returns>A collection of projects with rendered content added</returns>
        public static List<IVisualStudioProject> GenerateDynamicClasses(CodeGenSettings settings, List<DatabaseTable> tables, List<IVisualStudioProject> projects)
        {
            IProjectFileRender fileRender = new DynamicProjectFiles();
            projects = Generate<ICompiledTemplate, DatabaseTable>("ICompiledTemplate", settings, projects, fileRender, tables);
            return projects;
        }

        private static List<IVisualStudioProject> Generate<T,C>(string interfaceName, CodeGenSettings settings, List<IVisualStudioProject> projects, IProjectFileRender fileRender, IEnumerable<C> tables)
        {
            List<Assembly> assemblies = ReflectionHelper<T>.LoadAssemblies(settings.CompiledTemplateLocation);
            List<T> templates = ReflectionHelper<T>.LoadTemplates(assemblies, interfaceName);


            foreach (T template in templates)
            {
                IClassMetadata classmeta = (IClassMetadata)template;
                string projectNamespace = classmeta.ProjectNamespace;

                IVisualStudioProject projectFile = RetrieveProject(projectNamespace, projects);
                projectFile = fileRender.Render(settings, template, tables, projectFile);
                projects = SaveProject(projectFile, projects);
            }

            return projects;
        }

        public static void RenderProjects(IEnumerable<IVisualStudioProject> vsProjects)
        {
            foreach (IVisualStudioProject project in vsProjects)
            {
                project.Render();
            }
        }

        /// <summary>
        /// Search for project in the project collection. If project is found, it saves the changes. If it's not found then it adds the project to the collection.
        /// </summary>
        /// <param name="project">Recently changed project</param>
        /// <param name="vsProjects">Collection of projects to be searched</param>
        /// <returns>A collection of projects with the project changes added to the collection</returns>
        private static List<IVisualStudioProject> SaveProject(IVisualStudioProject project, List<IVisualStudioProject> vsProjects)
        {
            bool wasFound = false;

            //Search for project
            for (int index = 0; index < vsProjects.Count; index++ )
            {
                if (vsProjects[index].ProjectNamespace.ToLower() == project.ProjectNamespace.ToLower())
                {
                    vsProjects[index] = project;
                    wasFound = true;
                    break;
                }
            }

            //if it wasn't found add it to the project collection
            if(!wasFound)
            {
                vsProjects.Add(project);
            }

            return vsProjects;
        }

        /// <summary>
        /// Finds the project in the current project collection, if one is not found then a new one is created.
        /// </summary>
        /// <param name="projectNamespace"></param>
        /// <param name="vsProjects"></param>
        /// <returns></returns>
        public static IVisualStudioProject RetrieveProject(string projectNamespace, List<IVisualStudioProject> vsProjects)
        {

            foreach (IVisualStudioProject projects in vsProjects)
            {
                if (projects.ProjectNamespace.ToLower() == projectNamespace.ToLower())
                {
                    return projects;
                }
            }

            return new ClassLibrary();
        }

        /// <summary>
        /// Builds the namespace based on folder structure of the class.
        /// </summary>
        /// <param name="classmeta"></param>
        /// <returns></returns>
        public static string AddFolderNamespaces(IClassMetadata classmeta)
        {
            string namespaces = string.Empty;

            //Make sure it has more than an empty space
            if(!string.IsNullOrEmpty(classmeta.ClassFilePath))
            {
                string[] namespaceSegments = classmeta.ClassFilePath.Split('\\');

                //Make sure it's not an empty collection
                if (namespaceSegments.Length > 0)
                {
                    foreach (string segs in namespaceSegments)
                    {
                        namespaces += (!string.IsNullOrEmpty(segs) ? string.Format(".{0}", segs) : string.Empty);
                    }
                }
            }

            return namespaces;
        }

        public static IVisualStudioProject SetProjectProperties(CodeGenSettings settings, IClassMetadata classmeta, IVisualStudioProject projectFile, string projectDirectory)
        {
            //Set project properties
            projectFile.ProjectDirectory = projectDirectory;
            projectFile.RootNamespace = settings.SolutionNamespace;
            projectFile.ProjectNamespace = classmeta.ProjectNamespace;

            return projectFile;
        }

    }
}
