using DotLiquid;
using System.Collections.Generic;

namespace DotLiquidExtended
{
    public class RenderResult
    {
        public Template Template { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Result { get; set; }
    }
}