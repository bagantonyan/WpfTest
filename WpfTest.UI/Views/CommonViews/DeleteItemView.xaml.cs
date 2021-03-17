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
using WpfTest.UI.ViewModels.CommonViewModels;

namespace WpfTest.UI.Views.CommonViews
{
    /// <summary>
    /// Interaction logic for DeleteItemView.xaml
    /// </summary>
    public partial class DeleteItemView : Window
    {
        public DeleteItemView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;
        }
    }
}
