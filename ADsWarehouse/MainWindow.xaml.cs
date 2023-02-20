using ADsWarehouse.Classes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace ADsWarehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client server;
        BytesConvert BytesConvert;
        public MainWindow()
        {
            InitializeComponent();
            server  = new Client();
            BytesConvert = new();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = await server.ReturnObject<DataTable>(new object[] { "get", "" });
            dgv.ItemsSource = dataTable.DefaultView;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string dataTable = await server.ReturnObject<string>(new object[] { "usr", "" });
            MessageBox.Show(dataTable);
        }

 
    }
}
