using AppProyectoMulta.Controller;
using AppProyectoMulta.Model;
using System.Windows;
using System.Windows.Controls;
namespace AppProyectoMulta.View
{
    public partial class GestionTipoMulta : Window
    {
        TipoMultaController controller;
        public GestionTipoMulta()
        {
            InitializeComponent();

            controller = new TipoMultaController();

            CargarTipoMulta();
        }
        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }
        private void CargarTipoMulta()
        {
            dataGridTipoMulta.ItemsSource = controller.ObtenerTipoMulta();
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = " (Automático) ";
            txtDescripcion.Text = "";
            dataGridTipoMulta.SelectedItem = null;
        }
        

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.");
                return;
            }

            try
            {
                if (txtId.Text == " (Automático) ")
                {
                   controller.AgregarTipoMulta(txtDescripcion.Text);
                    MessageBox.Show("Tipo de Multa agregado correctamente");
                }
                else 
                { 
                    int id = Convert.ToInt32(txtId.Text);
                    controller.ActualizarTipoMulta(id, txtDescripcion.Text);
                    MessageBox.Show("Tipo de Multa Actualizada con exito.");
                }
                CargarTipoMulta();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Guarda: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTipoMulta.SelectedItem == null) 
            {
                MessageBox.Show("Debe de seleccionar un Tipo de Multa para Eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este Tipo de Multa?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if(resultado == MessageBoxResult.Yes)
            {
                try
                {
                    TipoMulta tipoMultaSeleccionado = (TipoMulta)dataGridTipoMulta.SelectedItem;
                    controller.EliminarTipoMulta(tipoMultaSeleccionado.IdTipoMulta);

                    CargarTipoMulta() ;
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Eliminar: " + ex.Message);
                }
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID numérico válido para Buscar.");
                return;
            }

            List<TipoMulta> resultado = controller.BuscarTipoMulta(id);

            dataGridTipoMulta.ItemsSource = resultado;

            if(resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún  Tipo de Multa con ese ID.", "Búsqueda sin Resultado");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarTipoMulta();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void DataGridTipoMulta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridTipoMulta.SelectedItem != null && dataGridTipoMulta.SelectedItem is TipoMulta tipoMulta)
            {
                txtId.Text = tipoMulta.IdTipoMulta.ToString();
                txtDescripcion.Text = tipoMulta.DescripcionTipoMulta.ToString();
            }
        }
    }
}
