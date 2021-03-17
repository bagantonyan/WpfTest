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
using WpfTest.UI.ViewModels.CustomerViewModels;

namespace WpfTest.UI.Views.CustomerViews
{
    /// <summary>
    /// Interaction logic for AddCustomerView.xaml
    /// </summary>
    public partial class AddCustomerView : Window
    {
        AddCustomerViewModel viewModel = new AddCustomerViewModel();
        public AddCustomerView()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.RequestClose += (s, e) => this.Close();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
