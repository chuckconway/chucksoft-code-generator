using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chucksoft.Entities.Execptions;

namespace Chucksoft.Entities
{
    public class VisualStudioSolution
    {
        public List<IVisualStudioProject> Projects { get; set; }

        private Guid solutionGuid;
        private Guid SolutionGuid()
        {
            if (solutionGuid == Guid.Empty)
            {
                solutionGuid = Guid.NewGuid();
            }

            return solutionGuid;
        }

        public void Render(string outputDirectory, string rootNamespace)
        {
            string contents = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(VisualStudioSolution), "Chucksoft.Entities.Templates.SolutionTemplate.template");

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(contents))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Entities.Templates.SolutionTemplate.template\"");
            }
            
            string solutionName = string.Format(@"{0}\{1}.sln", outputDirectory, rootNamespace);

            //Replace expected tokens in the template
            contents = contents.Replace("<%[Projects]%>", BuildProjectSection());
            contents = contents.Replace("<%[GlobalSection]%>", GenerateProjectConfigurationPlatformSection());

            //Embedded Resource is adding a weird character at the beginning of the stream. I'm manually removing it.
            //Not the best solution, but it works for now.
            //contents = contents.Remove(0, contents.IndexOf("Microsoft"));

            //clean the invalid character off the beginning
            contents = contents.Trim();

            //re-add the newline, otherwise visual studio get's angry and doesn't load.
            contents = contents.Insert(0, Environment.NewLine);

            UTF8Encoding encoding = new UTF8Encoding(true);

            File.WriteAllText(solutionName, contents, encoding);
        }


        private string BuildProjectSection()
        {
            StringBuilder builder = new StringBuilder();

            foreach (IVisualStudioProject project in Projects)
            {
                string fullyQualifiedNamespace = project.RootNamespace + "." + project.ProjectNamespace;

                builder.AppendLine("Project(\"{" + SolutionGuid() + "}\") = \"" + fullyQualifiedNamespace + "\", \"" + fullyQualifiedNamespace + @"\" + fullyQualifiedNamespace + ".csproj\", \"{" + project.ProjectGuid + "}\"" +
                 Environment.NewLine + "EndProject");
            }

            return builder.ToString();
        }

        private string GenerateProjectConfigurationPlatformSection()
        {
            StringBuilder builder = new StringBuilder();
            
            foreach (IVisualStudioProject project in Projects)
            {
                builder.AppendLine("{" + project.ProjectGuid + "}.Debug|Any CPU.ActiveCfg = Debug|Any CPU)");
                builder.AppendLine("{" + project.ProjectGuid + "}.Debug|Any CPU.Build.0 = Debug|Any CPU");
                builder.AppendLine("{" + project.ProjectGuid + "}.Release|Any CPU.ActiveCfg = Release|Any CPU");
                builder.AppendLine("{" + project.ProjectGuid + "}.Release|Any CPU.Build.0 = Release|Any CPU");
                
            }

            return builder.ToString();
        }
    }
}
