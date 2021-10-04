using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace DiagramDesigner
{
    public class DiagramViewModel : INPCBase, IDiagramViewModel
    {
        private ObservableCollection<SelectableDesignerItemViewModelBase> items = new ObservableCollection<SelectableDesignerItemViewModelBase>();

        public DiagramViewModel()
        {
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            RemoveItemCommand = new SimpleCommand(ExecuteRemoveItemCommand);
            ClearSelectedItemsCommand = new SimpleCommand(ExecuteClearSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            ToolBoxItemClickCommand = new SimpleCommand(ExecuteToolBoxItemClickCommand);

            Mediator.Instance.Register(this);
        }



        [MediatorMessageSink("DoneDrawingMessage")]
        public void OnDoneDrawingMessage(bool dummy)
        {
            foreach (var item in Items.OfType<DesignerItemViewModelBase>())
            {
                item.ShowConnectors = false;
            }
        }


        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand RemoveItemCommand { get; private set; }
        public SimpleCommand ClearSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand ToolBoxItemClickCommand { get; private set; }

        public ObservableCollection<SelectableDesignerItemViewModelBase> Items
        {
            get { return items; }
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {
            get { return Items.Where(x => x.IsSelected).ToList(); }
        }

        private ConnectionTypeEnum currentConnectionType = ConnectionTypeEnum.Inheritance;
        public ConnectionTypeEnum CurrentConnectionType
        {
            get
            {
                return currentConnectionType;
            }
            set
            {
                currentConnectionType = value;
                NotifyChanged("CurrentConnectionType");
            }
        }

        protected virtual void ExecuteAddItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                item.Parent = this;
                Items.Add(item);
            }

            if (parameter is ConnectorViewModel)
            {
                ConnectorViewModel item = (ConnectorViewModel)parameter;

                item.ConnectionType = currentConnectionType;

                //Reset
                if (item.IsFullConnection)
                {
                    System.Windows.Input.Mouse.OverrideCursor = null;
                    CurrentConnectionType = ConnectionTypeEnum.Inheritance;
                }
            }
        }

        private void ExecuteToolBoxItemClickCommand(object parameter)
        {
            var connectionTypeId = System.Convert.ToInt32(parameter);

            CurrentConnectionType = (ConnectionTypeEnum)System.Enum.Parse(typeof(ConnectionTypeEnum), connectionTypeId.ToString());

            System.Windows.Input.Mouse.OverrideCursor = new System.Windows.Input.Cursor(System.IO.Path.GetFullPath("./Images/assoc.cur"));
            //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        private void ExecuteRemoveItemCommand(object parameter)
        {
            if (parameter is SelectableDesignerItemViewModelBase)
            {
                SelectableDesignerItemViewModelBase item = (SelectableDesignerItemViewModelBase)parameter;
                items.Remove(item);
            }
        }

        private void ExecuteClearSelectedItemsCommand(object parameter)
        {
            foreach (SelectableDesignerItemViewModelBase item in Items)
            {
                item.IsSelected = false;
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            Items.Clear();
        }
    }
}
