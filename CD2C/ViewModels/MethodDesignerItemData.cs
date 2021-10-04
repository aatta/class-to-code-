using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CD2C.Common;
using CD2C.Common.Helpers;
using DiagramDesigner;

namespace CD2C
{
    /// <summary>
    /// This is passed to the PopupWindow.xaml window, where a DataTemplate is used to provide the
    /// ContentControl with the look for this data. This class is also used to allow
    /// the popup to be cancelled without applying any changes to the calling ViewModel
    /// whos data will be updated if the PopupWindow.xaml window is closed successfully
    /// </summary>
    public class MethodDesignerItemData : INPCBase, ISupportDataChanges, ISupportValidation
    {
        private readonly MethodModel _methodModel = null;
        private readonly ICommand _command = null;
        private readonly IUIVisualizerService visualiserService;

        public MethodDesignerItemData(MethodModel dataMemberModel)
        {
            _methodModel = dataMemberModel;

            ScopeData = ComboHelper.FromEnum<ScopeEnum>();
            TypeData = ComboHelper.FromEnum<TypeEnum>();

            TypeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));

            _command = new SimpleCommand(ExecuteShowDataChangeWindowCommand);
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
        }

        public List<ComboData<ScopeEnum>> ScopeData { get; } = null;
        public List<ComboData<TypeEnum>> TypeData { get; } = null;

        public MethodModel MethodModel
        {
            get
            {
                return _methodModel;
            }
        }

        public ICommand ShowDataChangeWindowCommand
        {
            get
            {
                return _command;
            }
        }

        public void ExecuteShowDataChangeWindowCommand(object parameter)
        {
            InputParameterModel inputParameter = new InputParameterModel();
            InputParameterDesignerItemData data = new InputParameterDesignerItemData(inputParameter);
            if (visualiserService.ShowDialog(data) == true)
            {
                this.MethodModel.InputParameters.Add(inputParameter);
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
