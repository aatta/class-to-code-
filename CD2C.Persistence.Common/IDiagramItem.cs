using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD2C.Common
{
    public interface IDiagramItem 
    {
        List<DiagramItemData> DesignerItems { get; set; }
        List<int> ConnectionIds { get; set; }
    }
}
