using CD2C.Common;
using DiagramDesigner;
using DiagramDesigner.Helpers;
using System;
using System.Collections.Generic;

namespace CD2C
{
    public class ToolBoxViewModel
    {
        private List<ToolBoxData> toolBoxItems = new List<ToolBoxData>();

        public ToolBoxViewModel(SimpleCommand itemClickCommand)
        {
            toolBoxItems.Add(new ToolBoxData("./Images/icon_class.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0, ConnectionTypeEnum.Inheritance, true));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_assoc.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0, ConnectionTypeEnum.Association));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_inherit.png", typeof(ClassDesignerItemViewModel), 173.0, 21.0, ConnectionTypeEnum.Inheritance));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_aggre.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0, ConnectionTypeEnum.Aggregation));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_comp.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0, ConnectionTypeEnum.Composition));

            ItemClickCommand = itemClickCommand;
        }

        public SimpleCommand ItemClickCommand { get; private set; }

        public List<ToolBoxData> ToolBoxItems
        {
            get { return toolBoxItems; }
        }
    }
}
