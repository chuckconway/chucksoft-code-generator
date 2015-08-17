﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Conway.IO;

namespace Chucksoft.Entities
{
    public class ClassLibrary : IVisualStudioProject
    {

        public ClassLibrary()
        {
            Classes = new List<ProjectArtifact>();
        }

 
        private Guid projectGuid;
        /// <summary>
        /// Generates new Guid for Project
        /// </summary>
        public Guid ProjectGuid
        {
            get
            {
               if(projectGuid == Guid.Empty)
               {
                   projectGuid = Guid.NewGuid();
               }

                return projectGuid;
            }
        }

        /// <summary>
        /// ProjectNamespace will be concatnated with RootNamespace. It also serve as a key.
        /// </summary>
        public string ProjectNamespace { get; set; }
        public string RootNamespace { get; set;}
        public string ProjectDirectory { get; set; }
        public List<ProjectArtifact> Classes { get; set; }
        public ProjectType VisualStudioProjectType { get; set; }

        public void Render()
        {
            
            string projectNamespace = string.Format("{0}.{1}", RootNamespace, ProjectNamespace);

            string contents = BuildClassLibrayProject(projectNamespace);
            string fullFileName = ProjectDirectory + @"\" + projectNamespace + ".csproj";
            if (!File.Exists(fullFileName))
            {
                File.WriteAllText(fullFileName, contents);
            }

        }

        private string BuildClassLibrayProject(string projectNamespace )
        {
            string contents = EmbeddedResource.ConvertStreamResourceToString(typeof(ClassLibrary), "Chucksoft.Entities.Templates.ClassLibraryProject.template");

            contents = contents.Replace("<%[ProjectGuid]%>", ProjectGuid.ToString());
            contents = contents.Replace("<%[RootNamespace]%>", projectNamespace);
            contents = contents.Replace("<%[AssemblyName]%>", projectNamespace);
            contents = contents.Replace("<%[ProjectClasses]%>", GenerateFiles());
            
            return contents;
            
        }

        private string GenerateFiles()
        {
            StringBuilder builder = new StringBuilder();

            foreach (ProjectArtifact artifact in Classes)
            {
                if (artifact.CreateGeneratedFile)
                {
                    builder.AppendLine(string.Format("<Compile Include=\"{0}\">", ComposeGeneratedFilename(artifact.Name)));
                    builder.AppendLine(string.Format("<DependentUpon>{0}</DependentUpon>", artifact.Name));
                    builder.AppendLine("</Compile>");
                }

                builder.AppendLine(string.Format("<Compile Include=\"{0}\" />", artifact.Name));
            }

            return builder.ToString();
        }

        public static string ComposeGeneratedFilename(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string file = fileName.Split('.')[0];

            string injectGenrated = string.Format("{0}.generated{1}", file, extension);

            return injectGenrated;
        }

    }
}