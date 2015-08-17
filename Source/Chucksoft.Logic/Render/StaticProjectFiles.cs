using System.Collections.Generic;
using System.IO;
using Chucksoft.Entities;
using Chucksoft.Entities.GenerationTemplates;
using Chucksoft.Entities.GenerationTemplates.Static;
using Chucksoft.Logic.Projects;

namespace Chucksoft.Logic.Render
{
    public class StaticProjectFiles: IProjectFileRender
    {

        #region IFileRender Members

        public IVisualStudioProject Render<T>(CodeGenSettings settings, object template, IEnumerable<T> collection, IVisualStudioProject projectFile)
        {
            IStaticAsset asset = (IStaticAsset) template;
            List<StaticContent> renderedAssests = asset.Render();


            IClassMetadata classmeta = asset;
            
            string projectNamespace = string.Format("{0}.{1}", settings.SolutionNamespace, classmeta.ProjectNamespace);
            string projectDirectory = string.Format(@"{0}\{1}", settings.CodeGenerationDirectory, projectNamespace);

            projectFile = ClassesLogic.SetProjectProperties(settings, classmeta, projectFile, projectDirectory);

            //create Directory if it doesn't exists
            if (!File.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }                
            
            //Set File properties... optional Namespace, and the full file path.
            string fullProjectNamespace = projectNamespace + ClassesLogic.AddFolderNamespaces(classmeta);
            

            foreach (StaticContent content in renderedAssests)
            {
                string filePath = string.Format(@"{0}\{1}{2}", projectDirectory, classmeta.ClassFilePath, content.FileName);
                string projectClassPath = (string.IsNullOrEmpty(classmeta.ClassFilePath)
                               ? content.FileName
                               : classmeta.ClassFilePath + content.FileName);

                ProjectArtifact artifact = new ProjectArtifact(projectClassPath, content.CreateGeneratedCounterpart);
                projectFile.Classes.Add(artifact);

                StaticContent _content = content;
                if (content.SetNamespace)
                {
                    _content.Content = SetNamespaceAndClass(content.Content.ToString(), fullProjectNamespace);
                }

                SaveContent(classmeta, projectDirectory, content, _content, fullProjectNamespace, filePath);
            }

            return projectFile;
        }

        private static void SaveContent(IClassMetadata classmeta, string projectDirectory, StaticContent content, StaticContent _content, string fullProjectNamespace, string filePath)
        {
            if (content.CreateGeneratedCounterpart)
            {
                //Get genertaed filename and path
                string generatedFileName = ClassLibrary.ComposeGeneratedFilename(content.FileName);
                string generatedFilePath = string.Format(@"{0}\{1}{2}", projectDirectory, classmeta.ClassFilePath, generatedFileName);

                //Get Generated template and inject the correct namespace
                string customClassContent = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(ClassLibrary), "Chucksoft.Entities.Templates.Custom.template");
                customClassContent = SetNamespaceAndClass(customClassContent, fullProjectNamespace);

                //write both files out
                File.WriteAllText(generatedFilePath, _content.Content.ToString());
                File.WriteAllText(filePath, customClassContent);
            }
            else
            {
                File.WriteAllText(filePath, _content.Content.ToString());
            }
        }

        private static string SetNamespaceAndClass(string content, string nameSpace)
        {
            content = content.Replace("<%[Namespace]%>", nameSpace);

            return content;
        }

        #endregion
    }

}
