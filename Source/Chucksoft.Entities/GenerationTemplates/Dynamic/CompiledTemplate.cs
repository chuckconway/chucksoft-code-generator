using Chucksoft.Entities.Database;

namespace Chucksoft.Entities.GenerationTemplates.Dynamic
{
    public abstract class CompiledTemplate : ICompiledTemplate
    {

        public abstract string RenderTemplate(DatabaseTable table, CodeGenSettings settings);

        /// <summary>
        /// Will be put in a project with your desired namespace. All the files that contain the same name will be put into the same project
        /// </summary>
        public string ProjectNamespace { get; set; }
        /// <summary>
        /// If not set or blank, then the class file will be put into the root of the project. If path is specified the class file will be generated starting at the root of the project.
        /// </summary>
        public string ClassFilePath { get; set; }

        /// <summary>
        /// Appends this string to the end of the each of the files
        /// </summary>
        public string FilenameAppending { get; set; }

    }
}