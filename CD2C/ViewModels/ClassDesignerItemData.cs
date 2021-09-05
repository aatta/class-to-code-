using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CD2C.Common;
using DiagramDesigner;

namespace DemoApp
{
    /// <summary>
    /// This is passed to the PopupWindow.xaml window, where a DataTemplate is used to provide the
    /// ContentControl with the look for this data. This class is also used to allow
    /// the popup to be cancelled without applying any changes to the calling ViewModel
    /// whos data will be updated if the PopupWindow.xaml window is closed successfully
    /// </summary>
    public class ClassDesignerItemData : INPCBase
    {
        private string _className = null;
        private ObservableCollection<MethodModel> _methods = null;
        private ObservableCollection<DataMemberModel> _dataMembers = null;



        public ClassDesignerItemData(string className, ObservableCollection<MethodModel> methods, ObservableCollection<DataMemberModel> dataMembers)
        {
            _className = className;
            _methods = methods;
            _dataMembers = dataMembers;
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
    }
}
