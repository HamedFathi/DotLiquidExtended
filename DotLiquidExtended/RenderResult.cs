using DotLiquid;
using System;
using System.Collections.Generic;

namespace DotLiquidExtended
{
    public class RenderResult
    {
        public Template Template { get; set; }
        public IEnumerable<Exception> Errors { get; set; }
        public string Result { get; set; }
    }
}