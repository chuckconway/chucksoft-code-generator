using System.Collections.Generic;
using System.IO;
using Chucksoft.Entities;
using Chucksoft.Entities.Database;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Dynamic;

namespace Chucksoft.Logic.Render
{
    public class DynamicProjectFiles: IProjectFileRender
    {
        #region IFileRender Members

        public IVisualStudioProject Render<T>(CodeGenSettings settings, object template, IEnumerable<T> collection, IVisualStudioProject projectFile)
        {
            ICompiledTemplate _template = (ICompiledTemplate)template;
            IEnumerable<DatabaseTable> tables = (IEnumerable<DatabaseTable>)collection;

            IClassMetadata classmeta = _template;
            
            //Creating project specific variables
            string projectNamespace = string.Format("{0}.{1}", settings.SolutionNamespace, classmeta.ProjectNamespace);
            string projectDirectory = string.Format(@"{0}\{1}", settings.CodeGenerationDirectory, projectNamespace);

            //Set the project properties. These should never change.
            ClassesLogic.SetProjectProperties(settings, classmeta, projectFile, projectDirectory);
            string fileDirectory = (string.IsNullOrEmpty(classmeta.ClassFilePath) ? projectDirectory : string.Format(@"{0}\{1}", projectDirectory, classmeta.ClassFilePath));

            //create the project and project sub directory
            CreateProjectAndClassDirectories(projectDirectory, fileDirectory);
            string fileNamespace = projectNamespace + ClassesLogic.AddFolderNamespaces(classmeta);

            //Render out the Templates by passing in the collection of Tables
            foreach (DatabaseTable table in tables)
            {
                string filename = table.Name + classmeta.FilenameAppending;
                string projectClassPath = (string.IsNullOrEmpty(classmeta.ClassFilePath)
                                               ? filename
                                               : classmeta.ClassFilePath + filename);
              projectFile.Classes.Add(new ProjectArtifact(projectClassPath + ".cs", true));

                string fullQualifiedPathOfClass = string.Format(@"{0}\{1}.cs", fileDirectory, filename);
                string content = _template.RenderTemplate(table, settings);

                //Write out Generated code
                content = SetNamespaceAndClass(content, filename, fileNamespace);
                File.WriteAllText(fullQualifiedPathOfClass, content);
            }

            return projectFile;
        }

        private static void CreateProjectAndClassDirectories(string projectDirectory, string fileDirectory)
        {
            //create projectFolder Directory if it doesn't exists
            if (!string.IsNullOrEmpty(projectDirectory) && !File.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }

            //create projectFolder Directory if it doesn't exists
            if (!string.IsNullOrEmpty(fileDirectory) && !File.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

        }

        /// <summary>
        /// Replace tokens in template
        /// </summary>
        /// <param name="content">content with tokens to be replaced</param>
        /// <param name="className">class name to replace "Class" token</param>
        /// <param name="nameSpace">namespace name to replace "Namespace" token</param>
        /// <returns>Content with replaced tokens.</returns>
        private static string SetNamespaceAndClass(string content, string className, string nameSpace)
        {
            content = content.Replace("<%[Namespace]%>", nameSpace);
            content = content.Replace("<%[Class]%>", className);

            return content;
        }

        #endregion
    }
}
