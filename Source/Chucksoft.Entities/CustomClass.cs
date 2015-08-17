using System.IO;
namespace Chucksoft.Entities
{
    public class CustomClass
    {
        public static string RetrieveTemplates(string fullTemplatePath)
        {
            string contents = string.Empty;

            if(File.Exists(fullTemplatePath))
            {
                contents = File.ReadAllText(fullTemplatePath);
            }

            return contents;
        }
    }
}
