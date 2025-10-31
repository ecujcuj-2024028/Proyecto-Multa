using AppProyectoMulta.View;
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

namespace AppProyectoMulta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Multas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Municipios_Click(object sender, RoutedEventArgs e)
        {
            GestionMunicipios ventanaMunicipios = new GestionMunicipios();

            ventanaMunicipios.Show();
            this.Close();
        }

        private void Dueños_Click(object sender, RoutedEventArgs e)
        {
            GestionDueño ventanaDueño = new GestionDueño();

            ventanaDueño.Show();
            this.Close();
        }

        private void Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            GestionVehiculo ventanaVehiculo = new GestionVehiculo();
            ventanaVehiculo.Show();
            this.Close();
        }

        private void Agentes_Click(object sender, RoutedEventArgs e)
        {
            GestionAgente ventanaAgente = new GestionAgente();
            ventanaAgente.Show();
            this.Close();
        }

        private void TiposMulta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pagos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}