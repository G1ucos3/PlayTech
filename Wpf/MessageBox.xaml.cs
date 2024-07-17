using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Brushes = System.Windows.Media.Brushes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }
        static MessageBox messageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum MessageBoxButton
        {
            Ok,
            No,
            Yes,
            Cancel,
            Confirm
        }

        public enum MessageBoxTittle
        {
            Error,
            Info,
            Warning,
            Confirm
        }

        public static DialogResult Show(string message, MessageBoxTittle title, MessageBoxButton btnOk, MessageBoxButton btnNo)
        {
            messageBox = new MessageBox();
            messageBox.txtMessage.Text = message;
            messageBox.btnOk.Content = messageBox.GetMessageButton(btnOk); 
            messageBox.btnCancel.Content = messageBox.GetMessageButton(btnNo);
            messageBox.txtTitle.Text = messageBox.GetTitle(title);

            switch (title)
            {
                case MessageBoxTittle.Error:
                    messageBox.msgIcon.Kind = PackIconKind.AlertCircle;
                    messageBox.iconMsg.Kind = PackIconKind.Error;
                    messageBox.iconMsg.Foreground = Brushes.Red;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case MessageBoxTittle.Info:
                    messageBox.bdTitle.Background = Brushes.Green;
                    messageBox.bdMain.BorderBrush = Brushes.Green;
                    messageBox.msgIcon.Kind = PackIconKind.InformationOutline;
                    messageBox.iconMsg.Kind = PackIconKind.InfoCircle;
                    messageBox.iconMsg.Foreground = Brushes.Blue;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case MessageBoxTittle.Warning:
                    messageBox.iconMsg.Kind = PackIconKind.Warning;
                    messageBox.iconMsg.Foreground = Brushes.Yellow;
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case MessageBoxTittle.Confirm:
                    messageBox.bdTitle.Background = Brushes.Purple;
                    messageBox.bdMain.BorderBrush = Brushes.Purple;
                    messageBox.msgIcon.Kind = PackIconKind.QuestionMark;
                    messageBox.iconMsg.Kind = PackIconKind.HelpCircle;
                    messageBox.iconMsg.Foreground = Brushes.Purple;
                    break;
            }
            messageBox.ShowDialog();
            return result;
        }

        public string GetTitle(MessageBoxTittle title)
        {
            return Enum.GetName(typeof(MessageBoxTittle), title);
        }

        public string GetMessageButton(MessageBoxButton button)
        {
            return Enum.GetName(typeof(MessageBoxButton), button);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            messageBox.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            messageBox.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
