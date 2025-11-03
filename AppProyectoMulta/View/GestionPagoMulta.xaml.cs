using AppProyectoMulta.Controllers;
using AppProyectoMulta.Model;
using System.Windows;
using System.Windows.Controls;

namespace AppProyectoMulta.View
{
    public partial class GestionPagoMulta : Window
    {
        private PagoMultaController controller;

        public GestionPagoMulta()
        {
            InitializeComponent();
            controller = new PagoMultaController();
            CargarPagos();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }

        private void CargarPagos()
        {
            dataGridPagos.ItemsSource = controller.ObtenerPagos();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtIdPago.Text = " (Automático) ";
            txtIdMulta.Text = "";
            dpFechaPago.SelectedDate = null;
            cbMetodoPago.SelectedIndex = -1;
            txtMontoPagado.Text = "";
            dataGridPagos.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdMulta.Text) ||
                dpFechaPago.SelectedDate == null ||
                string.IsNullOrEmpty(cbMetodoPago.Text) ||
                string.IsNullOrEmpty(txtMontoPagado.Text))
            {
                MessageBox.Show("Ningún campo puede estar vacío.");
                return;
            }

            try
            {
                int idMulta = int.Parse(txtIdMulta.Text);
                DateTime fecha = dpFechaPago.SelectedDate.Value;
                string metodo = cbMetodoPago.Text;
                decimal monto = decimal.Parse(txtMontoPagado.Text);

                if (txtIdPago.Text == " (Automático) ")
                {
                    controller.AgregarPago(idMulta, fecha, metodo, monto);
                    MessageBox.Show("Pago agregado con éxito.");
                }
                else
                {
                    int id = Convert.ToInt32(txtIdPago.Text);
                    controller.ActualizarPago(id, idMulta, fecha, metodo, monto);
                    MessageBox.Show("Pago actualizado con éxito.");
                }
                CargarPagos();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPagos.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un pago para eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este pago?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    PagoMulta pagoSeleccionado = (PagoMulta)dataGridPagos.SelectedItem;
                    controller.EliminarPago(pagoSeleccionado.IdPago);
                    CargarPagos();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void DataGridPagos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPagos.SelectedItem != null && dataGridPagos.SelectedItem is PagoMulta pago)
            {
                txtIdPago.Text = pago.IdPago.ToString();
                txtIdMulta.Text = pago.IdMulta.ToString();
                dpFechaPago.SelectedDate = pago.FechaPago;
                cbMetodoPago.Text = pago.MetodoPago;
                txtMontoPagado.Text = pago.MontoPagado.ToString("0.00");
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Ingrese un ID numérico válido para buscar.");
                return;
            }

            List<PagoMulta> resultado = controller.BuscarPago(id);
            dataGridPagos.ItemsSource = resultado;

            if (resultado.Count == 0)
                MessageBox.Show("No se encontró ningún pago con ese ID.");
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarPagos();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
