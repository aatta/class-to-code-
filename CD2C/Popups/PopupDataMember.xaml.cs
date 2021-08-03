using CD2C.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupDataMember : Window
    {
        List<ComboData> _scopeData = null;
        List<ComboData> _typeData = null;

        public CD2C.Common.DataMemberModel Result = null;

        public PopupDataMember()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _scopeData = ComboHelper.FromEnum(typeof(CD2C.Common.ScopeEnum));
            _typeData = ComboHelper.FromEnum(typeof(CD2C.Common.TypeEnum));

            cmbScope.ItemsSource = _scopeData;
            cmbType.ItemsSource = _typeData;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }


    }
}
