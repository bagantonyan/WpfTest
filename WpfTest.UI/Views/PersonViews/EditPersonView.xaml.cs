using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTest.UI.Views.PersonViews
{
    /// <summary>
    /// Interaction logic for EditPersonView.xaml
    /// </summary>
    public partial class EditPersonView : Window
    {
        public EditPersonView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
