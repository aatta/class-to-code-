using CD2C.Common;
using DiagramDesigner;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CD2C
{
    public class Window1ViewModel : INPCBase
    {

        private List<SelectableDesignerItemViewModelBase> itemsToRemove;
        private IMessageBoxService messageBoxService;
        private DiagramViewModel diagramViewModel = new DiagramViewModel();
        private bool isBusy = false;


        public Window1ViewModel()
        {
            messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;

            DiagramViewModel = new DiagramViewModel();
            ToolBoxViewModel = new ToolBoxViewModel(DiagramViewModel.ToolBoxItemClickCommand);

            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            GenerateCodeCommand = new SimpleCommand(ExecuteGenerateCodeCommand);

            //OrthogonalPathFinder is a pretty bad attempt at finding path points, it just shows you, you can swap this out with relative
            //ease if you wish just create a new IPathFinder class and pass it in right here
            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();

            DiagramViewModel.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var connections = e.NewItems.OfType<ConnectorViewModel>();
                foreach (var con in connections)
                {
                    con.PropertyChanged += Connection_PropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var connections = e.OldItems.OfType<ConnectorViewModel>();
                foreach (var con in connections)
                {
                    con.PropertyChanged -= Connection_PropertyChanged;
                }
            }
        }

        private void Connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var connection = sender as ConnectorViewModel;

            if (e.PropertyName == "IsSelected")
            {
                if (connection.IsSelected)
                {
                    ConnectionDesignerItemData data = new ConnectionDesignerItemData(connection);
                    if (ApplicationServicesProvider.Instance.Provider.VisualizerService.ShowDialog(data) == true)
                    {
                        
                    }
                }
            }
        }

        public SimpleCommand DeleteSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand GenerateCodeCommand { get; private set; }
        public ToolBoxViewModel ToolBoxViewModel { get; private set; }


        public DiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel != value)
                {
                    diagramViewModel = value;
                    NotifyChanged("DiagramViewModel");
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyChanged("IsBusy");
                }
            }
        }

        private void ExecuteDeleteSelectedItemsCommand(object parameter)
        {
            itemsToRemove = DiagramViewModel.SelectedItems;
            List<SelectableDesignerItemViewModelBase> connectionsToAlsoRemove = new List<SelectableDesignerItemViewModelBase>();

            foreach (var connector in DiagramViewModel.Items.OfType<ConnectorViewModel>())
            {
                if (ItemsToDeleteHasConnector(itemsToRemove, connector.SourceConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

                if (ItemsToDeleteHasConnector(itemsToRemove, (FullyCreatedConnectorInfo)connector.SinkConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

            }
            itemsToRemove.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in itemsToRemove)
            {
                DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            //ensure that itemsToRemove is cleared ready for any new changes within a session
            itemsToRemove = new List<SelectableDesignerItemViewModelBase>();
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);
        }

        private void ExecuteGenerateCodeCommand(object parameter)
        {
            if (!DiagramViewModel.Items.Any())
            {
                messageBoxService.ShowError("There must be at least one item in order generate code.");
                return;
            }

            IsBusy = true;
            DiagramItem wholeDiagram = new DiagramItem();

            Task<string> task = Task.Factory.StartNew<string>(() =>
            {
                //ensure that itemsToRemove is cleared ready for any new changes within a session
                itemsToRemove = new List<SelectableDesignerItemViewModelBase>();

                var generatedCode = ApplicationServicesProvider.Instance.Provider.CodeGeneratorService.GenerateCode(DiagramViewModel);

                return generatedCode;
            });
            task.ContinueWith((ant) =>
            {
                string generatedCode = ant.Result;

                //Save code
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = string.Format("{0}.cpp", "cd2c"); // Default file name
                dlg.DefaultExt = ".cpp"; // Default file extension
                dlg.Filter = "C++ code (.cpp)|*.cpp"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string fileName = dlg.FileName;

                    System.IO.File.WriteAllText(fileName, generatedCode);

                    //Environment.Exit(0);
                }

                IsBusy = false;
                //messageBoxService.ShowInformation(generatedCode);
                //messageBoxService.ShowInformation("Finished generating code.");

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItemViewModelBase> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }

    }
}
