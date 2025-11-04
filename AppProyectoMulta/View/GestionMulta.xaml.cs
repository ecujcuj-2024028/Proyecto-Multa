using AppProyectoMulta.Controllers;
using AppProyectoMulta.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AppProyectoMulta.View
{
    public partial class GestionMulta : Window
    {
        private MultaController controller;

        public GestionMulta()
        {
            InitializeComponent();
            controller = new MultaController();
            CargarMultas();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void CargarMultas()
        {
            dataGridMultas.ItemsSource = controller.ObtenerMultas();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "(Automático)";
            dpFechaMulta.SelectedDate = null;
            dpFechaLimite.SelectedDate = null;
            txtImporte.Text = "";
            txtLugar.Text = "";
            cbEstado.SelectedIndex = 0;
            txtIdVehiculo.Text = "";
            txtIdAgente.Text = "";
            txtIdTipoMulta.Text = "";
            dataGridMultas.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (dpFechaMulta.SelectedDate == null ||
                dpFechaLimite.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtImporte.Text) ||
                string.IsNullOrWhiteSpace(txtLugar.Text) ||
                string.IsNullOrWhiteSpace(txtIdVehiculo.Text) ||
                string.IsNullOrWhiteSpace(txtIdAgente.Text) ||
                string.IsNullOrWhiteSpace(txtIdTipoMulta.Text))
            {
                MessageBox.Show("Todos los campos deben estar completos.");
                return;
            }

            try
            {
                DateTime fechaMulta = dpFechaMulta.SelectedDate.Value;
                DateTime fechaLimite = dpFechaLimite.SelectedDate.Value;
                decimal importe = decimal.Parse(txtImporte.Text);
                string lugar = txtLugar.Text;
                string estado = ((ComboBoxItem)cbEstado.SelectedItem).Content.ToString();
                int idVehiculo = int.Parse(txtIdVehiculo.Text);
                int idAgente = int.Parse(txtIdAgente.Text);
                int idTipoMulta = int.Parse(txtIdTipoMulta.Text);

                if (txtId.Text == "(Automático)")
                {
                    controller.AgregarMulta(fechaMulta, fechaLimite, importe, lugar, estado, idVehiculo, idAgente, idTipoMulta);
                    MessageBox.Show("Multa agregada con éxito.");
                }
                else
                {
                    int id = int.Parse(txtId.Text);
                    controller.ActualizarMulta(id, fechaMulta, fechaLimite, importe, lugar, estado, idVehiculo, idAgente, idTipoMulta);
                    MessageBox.Show("Multa actualizada con éxito.");
                }

                CargarMultas();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar multa: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMultas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una multa para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea eliminar esta multa?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    Multa seleccionada = (Multa)dataGridMultas.SelectedItem;
                    controller.EliminarMulta(seleccionada.IdMulta);
                    CargarMultas();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar multa: " + ex.Message);
                }
            }
        }

        private void DataGridMultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridMultas.SelectedItem is Multa multa)
            {
                txtId.Text = multa.IdMulta.ToString();
                dpFechaMulta.SelectedDate = multa.FechaMulta;
                dpFechaLimite.SelectedDate = multa.FechaLimite;
                txtImporte.Text = multa.Importe.ToString();
                txtLugar.Text = multa.Lugar;
                cbEstado.Text = multa.Estado;
                txtIdVehiculo.Text = multa.IdVehiculo.ToString();
                txtIdAgente.Text = multa.IdAgente.ToString();
                txtIdTipoMulta.Text = multa.IdTipoMulta.ToString();
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Ingrese un ID numérico válido.");
                return;
            }

            List<Multa> resultado = controller.BuscarMulta(id);
            dataGridMultas.ItemsSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ninguna multa con ese ID.");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarMultas();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e) { }
    }
}
