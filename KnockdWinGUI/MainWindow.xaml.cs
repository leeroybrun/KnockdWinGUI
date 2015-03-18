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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;

namespace KnockdWinGUI
{

    public class Port : INotifyPropertyChanged
    {
        private int _Number;
        public int Number {
            get { return this._Number; }
            set
            {
                if (this._Number != value)
                {
                    this._Number = value;
                    this.NotifyPropertyChanged("Number");
                }
            }
        }

        private string _Protocol;
        public string Protocol
        {
            get { return this._Protocol; }
            set
            {
                if (this._Protocol != value)
                {
                    this._Protocol = value;
                    this.NotifyPropertyChanged("Protocol");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Port> PortsList = new ObservableCollection<Port>();

        public MainWindow()
        {
            InitializeComponent();

            PortsGrid.ItemsSource = this.PortsList;
        }

        private void AddPortButton_Click(object sender, RoutedEventArgs e)
        {
            this.PortsList.Add(new Port());
            PortsGrid.ItemsSource = this.PortsList;
        }

        private void RemovePortButton_Click(object sender, RoutedEventArgs e)
        {
            if(PortsGrid.SelectedItem != null)
            {
                PortsList.Remove(PortsGrid.SelectedItem as Port);
            }
        }

        private void SendPacketsButton_Click(object sender, RoutedEventArgs e)
        {
            if (PortsList.Count > 0 && IsValidIp(HostIPTextBox.Text))
            {
                List<Port> portsList = new List<Port>(PortsList);
                Knockd.Instance.Knock(HostIPTextBox.Text, portsList);
            }
            else
            {
                MessageBox.Show("Please enter a port sequence.");
            }
        }

        private void MoveUpPortButton_Click(object sender, RoutedEventArgs e)
        {
            if (PortsGrid.SelectedItem != null && PortsGrid.SelectedIndex > 0)
            {
                PortsList.Move(PortsGrid.SelectedIndex, PortsGrid.SelectedIndex - 1);
            }
        }

        private void MoveDownPortButton_Click(object sender, RoutedEventArgs e)
        {
            if (PortsGrid.SelectedItem != null && PortsGrid.SelectedIndex < (PortsList.Count-1))
            {
                PortsList.Move(PortsGrid.SelectedIndex, PortsGrid.SelectedIndex + 1);
            }
        }

        public bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }
    }
}
