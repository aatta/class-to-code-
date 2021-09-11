using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CD2C.Common;
using CD2C.Common.Helpers;
using DiagramDesigner;

namespace DemoApp
{
    /// <summary>
    /// This is passed to the PopupWindow.xaml window, where a DataTemplate is used to provide the
    /// ContentControl with the look for this data. This class is also used to allow
    /// the popup to be cancelled without applying any changes to the calling ViewModel
    /// whos data will be updated if the PopupWindow.xaml window is closed successfully
    /// </summary>
    public class MethodDesignerItemData : INPCBase, ISupportValidation
    {
        private MethodModel _methodModel = null;
        List<ComboData<ScopeEnum>> _scopeData = null;
        List<ComboData<TypeEnum>> _typeData = null;

        public MethodDesignerItemData(MethodModel dataMemberModel)
        {
            _methodModel = dataMemberModel;

            _scopeData = ComboHelper.FromEnum<ScopeEnum>();
            _typeData = ComboHelper.FromEnum<TypeEnum>();

            _typeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));
        }

        public List<ComboData<ScopeEnum>> ScopeData { get { return _scopeData; } }
        public List<ComboData<TypeEnum>> TypeData { get { return _typeData; } }

        public MethodModel MethodModel
        {
            get
            {
                return _methodModel;
            }
            set
            {
                if (_methodModel != value)
                {
                    _methodModel = value;
                    NotifyChanged("DataMemberModel");
                }
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this._methodModel.Name))
            {
                return false;
            }

            return true;
        }
    }
}
