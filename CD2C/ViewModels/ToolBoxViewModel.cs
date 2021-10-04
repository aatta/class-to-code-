using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DiagramDesigner.Helpers;
using DiagramDesigner;

namespace DemoApp
{
    public class ToolBoxViewModel
    {
        private List<ToolBoxData> toolBoxItems = new List<ToolBoxData>();

        public ToolBoxViewModel()
        {
            toolBoxItems.Add(new ToolBoxData("./Images/icon_class.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_assoc.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_inherit.png", typeof(ClassDesignerItemViewModel), 173.0, 21.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_aggre.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_comp.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
        }

        public List<ToolBoxData> ToolBoxItems
        {
            get { return toolBoxItems; }
        }
    }
}
