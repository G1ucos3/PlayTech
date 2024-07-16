using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UserControl = System.Windows.Controls.UserControl;

namespace Wpf.MVVM.View
{
    public class Member
    {
        public String Character { get; set; }
        public String Number { get; set; }
        public String Name { get; set; }
        public String Position { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
    }
    /// <summary>
    /// Interaction logic for A_UsersView.xaml
    /// </summary>
    public partial class A_UsersView : UserControl
    {
        public A_UsersView()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "A", Name = "AAA AA AAA", Email = "a@a.com", Position = "Senior", Phone = "123456789" });
            members.Add(new Member { Number = "2", Character = "B", Name = "BB BB BBB", Email = "a@a.com", Position = "Senior", Phone = "123456789" });
            members.Add(new Member { Number = "3", Character = "C", Name = "CCC CCC CC", Email = "a@a.com", Position = "Senior", Phone = "123456789" });
            members.Add(new Member { Number = "4", Character = "D", Name = "DDDD DDD", Email = "a@a.com", Position = "Senior", Phone = "123456789" });
            members.Add(new Member { Number = "5", Character = "E", Name = "EEE EEE", Email = "a@a.com", Position = "Senior", Phone = "123456789" });

            userDataGrid.ItemsSource = members;
        }
    }
}
