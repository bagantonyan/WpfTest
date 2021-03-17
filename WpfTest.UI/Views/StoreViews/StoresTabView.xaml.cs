using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.UI.ViewModels.StoreViewModels;

namespace WpfTest.UI.Views.StoreViews
{
    /// <summary>
    /// Interaction logic for StoresTabView.xaml
    /// </summary>
    public partial class StoresTabView : TabItem
    {
        public StoresTabView()
        {
            InitializeComponent();
            
            DataContext = new StoresTabViewModel();
        }
    }
}
