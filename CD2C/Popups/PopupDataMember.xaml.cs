using CD2C.Common;
using CD2C.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupDataMember : Window
    {
        List<ComboData<ScopeEnum>> _scopeData = null;
        List<ComboData<TypeEnum>> _typeData = null;

        public CD2C.Common.DataMemberModel Result = null;

        public PopupDataMember()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _scopeData = ComboHelper.FromEnum<ScopeEnum>();
            _typeData = ComboHelper.FromEnum<TypeEnum>();

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

            this.Result = new CD2C.Common.DataMemberModel
            {
                Name = name,
                Scope = scope,
                Type = type
            };

            this.DialogResult = true;
            this.Close();
        }


    }
}
