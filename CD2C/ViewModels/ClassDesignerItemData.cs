using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CD2C.Common;
using DiagramDesigner;

namespace CD2C
{
    /// <summary>
    /// This is passed to the PopupWindow.xaml window, where a DataTemplate is used to provide the
    /// ContentControl with the look for this data. This class is also used to allow
    /// the popup to be cancelled without applying any changes to the calling ViewModel
    /// whos data will be updated if the PopupWindow.xaml window is closed successfully
    /// </summary>
    public class ClassDesignerItemData : INPCBase, ISupportDataChanges, ISupportValidation
    {
        private string _className = null;
        private ObservableCollection<MethodModel> _methods = null;
        private ObservableCollection<DataMemberModel> _dataMembers = null;
        private ICommand _command = null;
        private readonly IUIVisualizerService visualiserService;


        public ClassDesignerItemData(string className, ObservableCollection<MethodModel> methods, ObservableCollection<DataMemberModel> dataMembers)
        {
            _className = className;

            //?? checks for null does assignment in one statement.
            _methods = methods != null ? new ObservableCollection<MethodModel>(methods) : new ObservableCollection<MethodModel>();
            _dataMembers = dataMembers != null ? new ObservableCollection<DataMemberModel>(dataMembers) : new ObservableCollection<DataMemberModel>();

            _command = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
        }

        public ICommand ShowDataChangeWindowCommand
        {
            get
            {
                return _command;
            }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    NotifyChanged("Command");
                }
            }
        }

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

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            string strParams = null;

            if (parameter != null)
            {
                strParams = parameter.ToString();
            }

            switch (strParams)
            {
                case "AddDataMember":
                    {
                        DataMemberModel dataMember = new DataMemberModel();
                        DataMemberDesignerItemData data = new DataMemberDesignerItemData(dataMember);
                        if (visualiserService.ShowDialog(data) == true)
                        {
                            this.DataMembers.Add(dataMember);
                        }
                        break;
                    }
                case "AddMethod":
                    {
                        MethodModel method = new MethodModel();
                        MethodDesignerItemData data = new MethodDesignerItemData(method);
                        if (visualiserService.ShowDialog(data) == true)
                        {
                            this.Methods.Add(method);
                        }
                        break;
                    }
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.ClassName))
            {
                return false;
            }

            return true;
        }
    }
}
