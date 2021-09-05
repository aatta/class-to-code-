using CD2C.Common;
using CD2C.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupMethod : Window
    {
        List<ComboData> _scopeData = null;
        List<ComboData> _typeData = null;

        public CD2C.Common.MethodModel Result = null;
        public Dictionary<string, TypeEnum> inputParameters = new Dictionary<string, TypeEnum>();

        public PopupMethod()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _scopeData = ComboHelper.FromEnum(typeof(CD2C.Common.ScopeEnum));
            _typeData = ComboHelper.FromEnum(typeof(CD2C.Common.TypeEnum));

            _typeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));

            cmbScope.ItemsSource = _scopeData;
            cmbType.ItemsSource = _typeData;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            if (cmbScope.SelectedItem == null)
            {
                MessageBox.Show("Scope not selected.");

                return;
            }

            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Type not selected.");

                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Name not entered.");

                return;
            }

            var name = txtName.Text;
            var scope = (ScopeEnum)Enum.Parse(typeof(ScopeEnum), ((int)cmbScope.SelectedValue).ToString());
            var type = (TypeEnum)Enum.Parse(typeof(TypeEnum), ((int)cmbType.SelectedValue).ToString());

            this.Result = new CD2C.Common.MethodModel
            {
                Name = name,
                Scope = scope,
                ReturnType = type,
                InputParameters = inputParameters
            };

            this.DialogResult = true;
            this.Close();
        }

        private void AddNewParameter_Click(object sender, RoutedEventArgs e)
        {
            var visualizerService = new WPFUIVisualizerService();

            var result = visualizerService.ShowInputParameterPopup(this);

            if (!string.IsNullOrEmpty(result.Key) && inputParameters.ContainsKey(result.Key))
            {
                MessageBox.Show("Name already exists.");

                return;
            }

            if (!string.IsNullOrEmpty(result.Key))
            {
                inputParameters.Add(result.Key, result.Value);

                dgInputParameters.Items.Add(result);
            }
        }
    }
}
