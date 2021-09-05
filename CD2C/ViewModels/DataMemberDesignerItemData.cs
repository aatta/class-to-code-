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
    public class DataMemberDesignerItemData : INPCBase, ISupportValidation
    {
        private DataMemberModel _dataMemberModel = null;
        List<ComboData> _scopeData = null;
        List<ComboData> _typeData = null;

        public DataMemberDesignerItemData(DataMemberModel dataMemberModel)
        {
            _dataMemberModel = dataMemberModel;

            _scopeData = ComboHelper.FromEnum(typeof(ScopeEnum));
            _typeData = ComboHelper.FromEnum(typeof(TypeEnum));

            _typeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));
        }

        public List<ComboData> ScopeData { get { return _scopeData; } }
        public List<ComboData> TypeData { get { return _typeData; } }

        public DataMemberModel DataMemberModel
        {
            get
            {
                return _dataMemberModel;
            }
            set
            {
                if (_dataMemberModel != value)
                {
                    _dataMemberModel = value;
                    NotifyChanged("DataMemberModel");
                }
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this._dataMemberModel.Name))
            {
                return false;
            }

            return true;
        }
    }
}
