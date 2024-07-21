﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for CreateNewComputer.xaml
    /// </summary>
    public partial class CreateNewComputer : Window, INotifyPropertyChanged
    {
        public Computer CurrentComputer { get; set; }
        private AcCommand submitCommand;

        public CreateNewComputer(Computer currentComputer)
        {
            InitializeComponent();
            CurrentComputer = currentComputer;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentComputer.PropertyChanged += CurrentComputer_PropertyChanged;
            CurrentComputer.ErrorsChanged += CurrentComputer_ErrorsChanged;
        }

        public AcCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
                OnPropertyChanged(nameof(SubmitCommand));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CurrentComputer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentComputer_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentComputer.ComputerName) &&
                !string.IsNullOrEmpty(CurrentComputer.ComputerSpec) &&
                !CurrentComputer.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CurrentComputer.HasErrors)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}