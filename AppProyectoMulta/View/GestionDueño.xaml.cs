using AppProyectoMulta.Controllers;
using AppProyectoMulta.Model;
using System.Windows;
using System.Windows.Controls;


namespace AppProyectoMulta.View
{
    public partial class GestionDueño : Window
    {
        private DueñoController controller;
        public GestionDueño()
        {
            InitializeComponent();

            controller = new DueñoController();

            CargaDueño();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }
        private void CargaDueño()
        {
            dataGridDueños.ItemsSource = controller.ObtenerDueños();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = " (Automático) ";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            dataGridDueños.SelectedItem = null;
        }
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtCorreo.Text) ||
                string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Ningun campo puede estar vacío.");
                return;
            }

            try
            {
                if (txtId.Text == " (Automático) ")
                {
                    controller.AgregarDueño(txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtCorreo.Text);
                    MessageBox.Show("Dueño agregado con éxito.");
                }
                else
                {
                    int id = Convert.ToInt32(txtId.Text);
                    controller.ActualizarDueño(id, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtCorreo.Text);
                    MessageBox.Show("Dueño actualizado con éxito.");
                }
                CargaDueño();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDueños.SelectedItem == null)
            {
                MessageBox.Show("Debe de seleccionar un Dueño para eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este dueño?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    Dueño dueñoSeleccionado = (Dueño)dataGridDueños.SelectedItem;
                    controller.EliminarDueño(dueñoSeleccionado.IdDueño);

                    CargaDueño();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void DataGridDueños_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridDueños.SelectedItem != null && dataGridDueños.SelectedItem is Dueño dueño)
            {
                txtId.Text = dueño.IdDueño.ToString();
                txtNombre.Text = dueño.NombreDueño;
                txtDireccion.Text = dueño.Direccion;
                txtCorreo.Text = dueño.Correo;
                txtTelefono.Text = dueño.Telefono;
            }

        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID numérico válido para buscar.", "Error de Búsqueda");
                return;
            }
            List<Dueño> resultado = controller.BuscarDueño(id);

            dataGridDueños.ItemsSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún Dueño con ese ID.", "Búsqueda sin Resultado");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargaDueño();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
