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
using WpfTest.UI.ViewModels.PersonViewModels;

namespace WpfTest.UI.Views.PersonViews
{
    /// <summary>
    /// Interaction logic for AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : Window
    {
        AddPersonViewModel viewModel = new AddPersonViewModel();
        public AddPersonView()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.RequestClose += (s, e) => this.Close();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
