using CD2C.Common;
using CD2C.Common.Helpers;
using DiagramDesigner;
using System.Collections.Generic;

namespace CD2C
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
        List<ComboData<ScopeEnum>> _scopeData = null;
        List<ComboData<TypeEnum>> _typeData = null;

        public DataMemberDesignerItemData(DataMemberModel dataMemberModel)
        {
            _dataMemberModel = dataMemberModel;

            _scopeData = ComboHelper.FromEnum<ScopeEnum>();
            _typeData = ComboHelper.FromEnum<TypeEnum>();

            _typeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));
        }

        public List<ComboData<ScopeEnum>> ScopeData { get { return _scopeData; } }
        public List<ComboData<TypeEnum>> TypeData { get { return _typeData; } }

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
