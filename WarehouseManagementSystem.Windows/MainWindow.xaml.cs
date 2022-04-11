using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public OrderProcessor Processor { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            #region Populate the UI
            Orders.ItemsSource = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));
            #endregion
            Processor = new();
        }

        private void ProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;

            var reciept = new RecieptWindow(Processor);
            reciept.Owner = this;
            reciept.Show();

            if(order is not null)
            Processor.Process(order);

        }
    }
}
