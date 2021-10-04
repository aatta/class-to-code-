using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using CD2C.Common;
using System.Collections.ObjectModel;

namespace CD2C
{
    public class ClassDesignerItemViewModel : DesignerItemViewModelBase, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public ClassDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, string className, ObservableCollection<MethodModel> methods, ObservableCollection<DataMemberModel> dataMembers)
            : base(id, parent, left, top)
        {

            ClassName = className;
            Methods = methods;
            DataMembers = dataMembers;

            Init();
        }

        public ClassDesignerItemViewModel(int id, IDiagramViewModel parent, double left, double top, double itemWidth, double itemHeight, string className, ObservableCollection<MethodModel> methods, ObservableCollection<DataMemberModel> dataMembers)
             : base(id, parent, left, top, itemWidth, itemHeight)
        {

            ClassName = className;
            Methods = methods;
            DataMembers = dataMembers;
            Init();
        }

        public ClassDesignerItemViewModel()
        {
            Init();
        }

        private string _className = null;
        private ObservableCollection<MethodModel> _methods = null;
        private ObservableCollection<DataMemberModel> _dataMembers = null;

        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                if (_className != value)
                {
                    _className = value;
                    NotifyChanged("ClassName");
                }
            }
        }


        public ObservableCollection<MethodModel> Methods
        {
            get
            {
                return _methods;
            }
            set
            {
                if (_methods != value)
                {
                    _methods = value;
                    NotifyChanged("Methods");
                }
            }
        }

        public ObservableCollection<DataMemberModel> DataMembers
        {
            get
            {
                return _dataMembers;
            }
            set
            {
                if (_dataMembers != value)
                {
                    _dataMembers = value;
                    NotifyChanged("DataMembers");
                }
            }
        }

        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            ClassDesignerItemData data = new ClassDesignerItemData(this.ClassName, this.Methods, this.DataMembers);
            if (visualiserService.ShowDialog(data) == true)
            {
                this.ClassName = data.ClassName;
                this.Methods = data.Methods;
                this.DataMembers = data.DataMembers;
            }
        }

        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;

            this.ItemHeight = 450;
            this.ItemWidth = 150;

            if (string.IsNullOrEmpty(ClassName))
            {
                ClassName = "Class";
            }

            if (Methods == null)
            {
                Methods = new ObservableCollection<MethodModel>();
            }

            if (DataMembers == null)
            {
                DataMembers = new ObservableCollection<DataMemberModel>();
            }
        }
    }
}
