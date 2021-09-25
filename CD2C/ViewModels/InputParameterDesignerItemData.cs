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
    public class InputParameterDesignerItemData : INPCBase, ISupportValidation
    {
        private InputParameterModel _inputParameterModel = null;

        public InputParameterDesignerItemData(InputParameterModel inputParameterModel)
        {
            _inputParameterModel = inputParameterModel;
            TypeData = ComboHelper.FromEnum<TypeEnum>();

            TypeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));
        }

        public List<ComboData<TypeEnum>> TypeData { get; } = null;

        public InputParameterModel InputParameterModel
        {
            get => _inputParameterModel;
            set
            {
                if (_inputParameterModel != value)
                {
                    _inputParameterModel = value;
                    NotifyChanged("InputParameterModel");
                }
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this._inputParameterModel.Name))
            {
                return false;
            }

            return true;
        }
    }
}
