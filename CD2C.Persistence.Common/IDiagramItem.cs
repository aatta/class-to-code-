using System.Collections.Generic;

namespace CD2C.Common
{
    public interface IDiagramItem
    {
        List<DiagramItemData> DesignerItems { get; set; }
        List<int> ConnectionIds { get; set; }
    }
}
