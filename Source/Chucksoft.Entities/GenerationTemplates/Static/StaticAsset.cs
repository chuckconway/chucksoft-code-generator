using System.Collections.Generic;
using Chucksoft.Entities.GenerationTemplates.Static;

namespace Chucksoft.Entities.GenerationTemplates.Static
{
    public abstract class StaticProjectAsset : IStaticAsset
    {
        public abstract List<StaticContent> Render();

        /// <summary>
        /// Will be put in a project with your desired namespace. All the files that contain the same name will be put into the same project
        /// </summary>
        public string ProjectNamespace { get; set; }
        /// <summary>
        /// If not set or blank, then the class file will be put into the root of the project. If path is specified the class file will be generated starting at the root of the project.
        /// </summary>
        public string ClassFilePath { get; set; }

        /// <summary>
        /// This property is not used in the StaticAsset Class
        /// </summary>
        public string FilenameAppending { get; set; }

    }
}