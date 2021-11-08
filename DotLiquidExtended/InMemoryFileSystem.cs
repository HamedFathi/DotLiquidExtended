using DotLiquid;
using DotLiquid.FileSystems;
using System;
using System.Collections.Generic;

namespace DotLiquidExtended
{
    public class InMemoryFileSystem : IFileSystem
    {
        private static readonly object Lock = new object();
        private readonly Dictionary<string, string> _includes;
        public InMemoryFileSystem(Dictionary<string, string> includes)
        {
            _includes = includes;
        }
        public string ReadTemplateFile(Context context, string templateName)
        {
            lock (Lock)
            {
                if (string.IsNullOrEmpty(templateName))
                {
                    throw new ArgumentNullException(nameof(templateName), "Template name must be set.");
                }
                var len = templateName.Length;
                templateName = templateName[0] == '"' ? templateName.Substring(1) : templateName;
                templateName = templateName[len - 1] == '"' ? templateName.Substring(0, len - 1) : templateName;
                return _includes.ContainsKey(templateName) ? _includes[templateName] : "The key not found";
            }
        }
    }
}
