using Chucksoft.Entities.Database;

namespace Chucksoft.Entities.GenerationTemplates.Dynamic
{
    public interface ICompiledTemplate : IClassMetadata
    {
        string RenderTemplate(DatabaseTable table, CodeGenSettings settings);
    }
}