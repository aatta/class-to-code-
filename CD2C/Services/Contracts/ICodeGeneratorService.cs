using DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD2C
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(DiagramViewModel diagramViewModel);
    }
}
