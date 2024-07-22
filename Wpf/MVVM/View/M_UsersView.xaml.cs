using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.MVVM.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace Wpf.MVVM.View
{
    /// <summary>
    /// Interaction logic for M_UsersView.xaml
    /// </summary>
    /// 

    public partial class M_UsersView : UserControl
    {

        private M_UsersViewModel M_UsersViewModel;

        public M_UsersView()
        {
            InitializeComponent();
            M_UsersViewModel = new M_UsersViewModel(new UserService(), new CurrentComputerService(), new ComputerService());
            DataContext = M_UsersViewModel;
        }
    }
}
