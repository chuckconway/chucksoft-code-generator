using System;
using System.Collections.Generic;

namespace Chucksoft.Entities
{
    public interface IVisualStudioProject
    {
        Guid ProjectGuid { get;}
        string RootNamespace { get; set;}
        string ProjectNamespace { get; set; }
        string ProjectDirectory { get; set; }
        ProjectType VisualStudioProjectType { get; set; }

        List<ProjectArtifact> Classes { get; set; }

        void Render();
    }

    public enum ProjectType
    {
        ClassLibrary
    }
}
