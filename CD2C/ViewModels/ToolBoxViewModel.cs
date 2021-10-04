using DiagramDesigner;
using DiagramDesigner.Helpers;
using System;
using System.Collections.Generic;

namespace CD2C
{
    public class ToolBoxViewModel
    {
        private List<ToolBoxData> toolBoxItems = new List<ToolBoxData>();

        public ToolBoxViewModel()
        {
            toolBoxItems.Add(new ToolBoxData("./Images/icon_class.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0, true));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_assoc.cur", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_inherit.png", typeof(ClassDesignerItemViewModel), 173.0, 21.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_aggre.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));
            toolBoxItems.Add(new ToolBoxData("./Images/icon_comp.png", typeof(ClassDesignerItemViewModel), 173.0, 53.0));

            ItemClickCommand = new SimpleCommand(ExecuteItemClickCommand);
        }

        private void ExecuteItemClickCommand(object parameter)
        {
            var toolBoxItem = parameter as ToolBoxData;

            System.Windows.Input.Mouse.OverrideCursor = new System.Windows.Input.Cursor(System.IO.Path.GetFullPath(toolBoxItem.ImageUrl));
        }

        public SimpleCommand ItemClickCommand { get; private set; }

        public List<ToolBoxData> ToolBoxItems
        {
            get { return toolBoxItems; }
        }
    }
}
