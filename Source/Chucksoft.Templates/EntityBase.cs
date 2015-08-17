using System.Collections.Generic;
using Chucksoft.Entities.Execptions;
using Chucksoft.Entities.GenerationTemplates.Static;

namespace Chucksoft.Templates
{
    public class EntityBase : StaticProjectAsset
    {
        public EntityBase()
        {
            ProjectNamespace = "Entities";
            //ClassFilePath = @"Data\";
        }

        public override List<StaticContent> Render()
        {
            List<StaticContent> assets = new List<StaticContent>();

            string embeddedContent = Core.ResourceFileHelper.ConvertStreamResourceToUTF8String(typeof(ResourceAssets), "Chucksoft.Templates.Templates.EntityBase.template");

            //Check for expected content, if not found, badness has happen.
            if (string.IsNullOrEmpty(embeddedContent))
            {
                throw new ContentNotFound("Can't find embeddedResource \"Chucksoft.Templates.Templates.EntityBase.template\"");
            }
            
            StaticContent content = new StaticContent
                                                {
                                                    FileName = "EntityBase.cs",
                                                    SetNamespace = true,
                                                    Content = embeddedContent,
                                                    CreateGeneratedCounterpart = false
                                                };
            assets.Add(content);

            return assets;
        }
    }
}
