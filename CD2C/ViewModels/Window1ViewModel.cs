using CD2C.Common;
using DiagramDesigner;
using System;
using System.Collections.Generic;
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

            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new DiagramViewModel();

            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            GenerateCodeCommand = new SimpleCommand(ExecuteGenerateCodeCommand);

            //OrthogonalPathFinder is a pretty bad attempt at finding path points, it just shows you, you can swap this out with relative
            //ease if you wish just create a new IPathFinder class and pass it in right here
            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();
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

                ApplicationServicesProvider.Instance.Provider.MessageBoxService.ShowInformation(generatedCode);

                IsBusy = false;
                messageBoxService.ShowInformation("Finished generating code.");

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItemViewModelBase> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }

    }
}
