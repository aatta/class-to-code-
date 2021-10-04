using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DiagramDesigner.Helpers;
using DiagramDesigner;
using System.ComponentModel;
using System.Windows.Data;
using CD2C.Common;
using System.Threading.Tasks;
using System.Windows;

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

            Task<int> task = Task.Factory.StartNew<int>(() =>
            {
                //ensure that itemsToRemove is cleared ready for any new changes within a session
                itemsToRemove = new List<SelectableDesignerItemViewModelBase>();

                ExtractClassesFromDesigner(wholeDiagram, DiagramViewModel);

                return wholeDiagram.Id;
            });
            task.ContinueWith((ant) =>
            {
                int wholeDiagramId = ant.Result;

                IsBusy = false;
                messageBoxService.ShowInformation(string.Format("Finished saving Diagram Id : {0}", wholeDiagramId));

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void ExtractClassesFromDesigner(IDiagramItem wholeDiagram, IDiagramViewModel diagramViewModel)
        {
            //Add all PersistDesignerItemViewModel
            foreach (var persistItemVM in diagramViewModel.Items.OfType<ClassDesignerItemViewModel>())
            {
                //PersistDesignerItem persistDesignerItem = new PersistDesignerItem(persistItemVM.Id, persistItemVM.Left, persistItemVM.Top, persistItemVM.ItemWidth, persistItemVM.ItemHeight, persistItemVM.HostUrl);
                //wholeDiagram.DesignerItems.Add(new DiagramItemData(persistDesignerItem.Id, typeof(PersistDesignerItem)));
            }
            ////Add all SettingsDesignerItemViewModel
            //foreach (var settingsItemVM in diagramViewModel.Items.OfType<SettingsDesignerItemViewModel>())
            //{
            //    SettingsDesignerItem settingsDesignerItem = new SettingsDesignerItem(settingsItemVM.Id, settingsItemVM.Left, settingsItemVM.Top, settingsItemVM.ItemWidth, settingsItemVM.ItemHeight, settingsItemVM.Setting1);
            //    wholeDiagram.DesignerItems.Add(new DiagramItemData(settingsDesignerItem.Id, typeof(SettingsDesignerItem)));
            //}
            ////Add all GroupingDesignerItemViewModel
            //foreach (var groupingItemVM in diagramViewModel.Items.OfType<GroupingDesignerItemViewModel>())
            //{
            //    GroupDesignerItem groupDesignerItem = new GroupDesignerItem(groupingItemVM.Id, groupingItemVM.Left, groupingItemVM.Top, groupingItemVM.ItemWidth, groupingItemVM.ItemHeight);
            //    if(groupingItemVM.Items != null && groupingItemVM.Items.Count > 0)
            //    {
            //        GenerateCodeForDesignerItem(groupDesignerItem, groupingItemVM);
            //    }
            //    wholeDiagram.DesignerItems.Add(new DiagramItemData(groupDesignerItem.Id, typeof(GroupDesignerItem)));
            //}
            //Add all connections which should now have their Connection.DataItems filled in with correct Ids
            foreach (var connectionVM in diagramViewModel.Items.OfType<ConnectorViewModel>())
            {
                FullyCreatedConnectorInfo sinkConnector = connectionVM.SinkConnectorInfo as FullyCreatedConnectorInfo;

                Connection connection = new Connection(
                    connectionVM.Id,
                    connectionVM.SourceConnectorInfo.DataItem.Id,
                    GetOrientationFromConnector(connectionVM.SourceConnectorInfo.Orientation),
                    GetTypeOfDiagramItem(connectionVM.SourceConnectorInfo.DataItem),
                    sinkConnector.DataItem.Id,
                    GetOrientationFromConnector(sinkConnector.Orientation),
                    GetTypeOfDiagramItem(sinkConnector.DataItem));

                wholeDiagram.ConnectionIds.Add(connectionVM.Id);
            }
        }

        private Type GetTypeOfDiagramItem(DesignerItemViewModelBase vmType)
        {
            if (vmType is ClassDesignerItemViewModel)
                return typeof(ClassDesignerItem);


            throw new InvalidOperationException(string.Format("Unknown diagram type. Currently only {0} and {1} are supported",
                typeof(ClassDesignerItem).AssemblyQualifiedName,
                typeof(ClassDesignerItemViewModel).AssemblyQualifiedName
                ));

        }


        private Orientation GetOrientationFromConnector(ConnectorOrientation connectorOrientation)
        {
            Orientation result = Orientation.None;
            switch (connectorOrientation)
            {
                case ConnectorOrientation.Bottom:
                    result = Orientation.Bottom;
                    break;
                case ConnectorOrientation.Left:
                    result = Orientation.Left;
                    break;
                case ConnectorOrientation.Top:
                    result = Orientation.Top;
                    break;
                case ConnectorOrientation.Right:
                    result = Orientation.Right;
                    break;
            }
            return result;
        }

        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItemViewModelBase> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }

    }
}
