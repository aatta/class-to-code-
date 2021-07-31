using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;


namespace DemoApp
{
    public class ClassDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public ClassDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, string setting1)
            : base(id, parent, left, top)
        {

            this.Setting1 = setting1;

            Init();
        }

        public ClassDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string setting1)
             : base(id, parent, left, top, itemWidth, itemHeight)
        {

            this.Setting1 = setting1;
            Init();
        }

        public ClassDesignerItemViewModel()
        {
            Init();
        }

        public String Setting1 { get; set; }

        public List<string> Methods { get; set; }

        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            ClassDesignerItemData data = new ClassDesignerItemData(Setting1);
            if (visualiserService.ShowDialog(data) == true)
            {
                this.Setting1 = data.Setting1;
            }
        }

        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;

            this.ItemHeight = 200;
            this.ItemWidth = 150;

            Methods = new List<string>();

            for (int i = 1; i <= 10; i++)
            {
                Methods.Add(string.Format("Method{0}", i));
            }
        }
    }
}
