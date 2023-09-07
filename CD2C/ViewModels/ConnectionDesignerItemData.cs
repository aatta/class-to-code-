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
    public class ConnectionDesignerItemData : INPCBase, ISupportValidation
    {
        private ConnectorViewModel _connector = null;

        public ConnectionDesignerItemData(ConnectorViewModel connector)
        {
            _connector = connector;
            ConnectionTypeData = ComboHelper.FromEnum<ConnectionTypeEnum>();
            MultiplicityTypeData = ComboHelper.FromEnum<MultiplicityTypeEnum>();
        }

        public List<ComboData<ConnectionTypeEnum>> ConnectionTypeData { get; } = null;
        public List<ComboData<MultiplicityTypeEnum>> MultiplicityTypeData { get; } = null;

        public ConnectorViewModel ConnectorView
        {
            get => _connector;
            set
            {
                if (_connector != value)
                {
                    _connector = value;
                    NotifyChanged("ConnectorView");
                }
            }
        }

        public bool Validate()
        {
            if (this._connector == null)
            {
                return false;
            }

            return true;
        }
    }
}
