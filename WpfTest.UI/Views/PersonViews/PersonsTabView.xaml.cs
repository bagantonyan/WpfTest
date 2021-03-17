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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.UI.ViewModels.PersonViewModels;

namespace WpfTest.UI.Views.PersonViews
{
    /// <summary>
    /// Interaction logic for PersonsTabView.xaml
    /// </summary>
    public partial class PersonsTabView : TabItem
    {
        public PersonsTabView()
        {
            InitializeComponent();
            DataContext = new PersonsTabViewModel();
        }
    }
}
