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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Multas_Click(object sender, RoutedEventArgs e)
        {
            GestionMulta ventanaMulta = new GestionMulta();
            ventanaMulta.Show();
            this.Close();
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
            GestionTipoMulta ventanaTipoMulta = new GestionTipoMulta();
            ventanaTipoMulta.Show();
            this.Close();
        }

        private void Pagos_Click(object sender, RoutedEventArgs e)
        {
            GestionPagoMulta ventanaPago = new GestionPagoMulta();
            ventanaPago.Show();
            this.Close();
        }
    }
}