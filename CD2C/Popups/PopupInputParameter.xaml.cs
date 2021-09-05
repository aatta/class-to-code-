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
    public partial class PopupInputParameter : Window
    {
        List<ComboData> _typeData = null;

        public KeyValuePair<string, TypeEnum> Result;

        public PopupInputParameter()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _typeData = ComboHelper.FromEnum(typeof(CD2C.Common.TypeEnum));

            _typeData.ForEach(t => t.Text = t.Text.Replace("Type", string.Empty));

            cmbType.ItemsSource = _typeData;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
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
            var type = (TypeEnum)Enum.Parse(typeof(TypeEnum), ((int)cmbType.SelectedValue).ToString());

            this.Result = new KeyValuePair<string, TypeEnum>(name, type);

            this.DialogResult = true;
            this.Close();
        }


    }
}
