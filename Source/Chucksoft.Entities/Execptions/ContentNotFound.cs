using System;

namespace Chucksoft.Entities.Execptions
{
    public class ContentNotFound : Exception
    {
        public ContentNotFound(string message) : base(message) {}
    }
}
