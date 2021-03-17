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
using System.Windows.Shapes;
using WpfTest.UI.ViewModels.StoreViewModels;

namespace WpfTest.UI.Views.StoreViews
{
    /// <summary>
    /// Interaction logic for AddStoreView.xaml
    /// </summary>
    public partial class AddStoreView : Window
    {
        AddStoreViewModel viewModel = new AddStoreViewModel();
        public AddStoreView()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.RequestClose += (s, e) => this.Close();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
