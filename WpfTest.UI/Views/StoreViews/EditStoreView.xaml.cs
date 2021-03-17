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
using WpfTest.UI.ViewModels.StoreViewModels;

namespace WpfTest.UI.Views.StoreViews
{
    /// <summary>
    /// Interaction logic for EditStoreView.xaml
    /// </summary>
    public partial class EditStoreView : Window
    {
        public EditStoreView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
