namespace Chucksoft.Entities
{
    public class CodeGenSettings
    {
        public string DatabaseConnectionString { get; set; }
        public string SolutionNamespace { get; set; }
        public string CompiledTemplateLocation { get; set; }
        public string CodeGenerationDirectory { get; set; }
        public bool UseDynamicParameters { get; set; }
        public bool ReturnIdentityFromInserts { get; set; }

    }
}
