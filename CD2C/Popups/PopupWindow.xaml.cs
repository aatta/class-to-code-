using System.Windows;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ISupportValidation)
            {
                if (!(this.DataContext as ISupportValidation).Validate())
                {
                    return;
                }
            }
            this.DialogResult = true;
            this.Close();
        }


    }
}
