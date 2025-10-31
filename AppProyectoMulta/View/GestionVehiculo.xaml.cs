using AppProyectoMulta.Controller;
using AppProyectoMulta.Controllers;
using AppProyectoMulta.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AppProyectoMulta.View
{
    public partial class GestionVehiculo : Window
    {
        private VehiculoController controller;

        public GestionVehiculo()
        {
            InitializeComponent();
            controller = new VehiculoController();
            CargarVehiculo();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }

        private void CargarVehiculo()
        {
            dataGridVehiculo.ItemsSource = controller.ObtenerVehiculo();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = " (Automático) ";
            txtPlaca.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtColor.Text = "";
            txtAnio.Text = "";
            txtIdDueño.Text = "";
            dataGridVehiculo.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlaca.Text) ||
                string.IsNullOrEmpty(txtMarca.Text) ||
                string.IsNullOrEmpty(txtModelo.Text) ||
                string.IsNullOrEmpty(txtColor.Text) ||
                string.IsNullOrEmpty(txtAnio.Text) ||
                string.IsNullOrEmpty(txtIdDueño.Text))
            {
                MessageBox.Show("Ningún campo puede estar vacío.");
                return;
            }

            try
            {
                if (txtId.Text == " (Automático) ")
                {
                    controller.AgregarVehiculo(
                        txtPlaca.Text,
                        txtMarca.Text,
                        txtModelo.Text,
                        txtColor.Text,
                        Convert.ToInt32(txtAnio.Text),
                        Convert.ToInt32(txtIdDueño.Text)
                    );
                    MessageBox.Show("Vehículo agregado con éxito.");
                }
                else
                {
                    int id = Convert.ToInt32(txtId.Text);
                    controller.ActualizarVehiculo(
                        id,
                        txtPlaca.Text,
                        txtMarca.Text,
                        txtModelo.Text,
                        txtColor.Text,
                        Convert.ToInt32(txtAnio.Text),
                        Convert.ToInt32(txtIdDueño.Text)
                    );
                    MessageBox.Show("Vehículo actualizado con éxito.");
                }

                CargarVehiculo();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVehiculo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un vehículo para eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este vehículo?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    Vehiculo vehiculoSeleccionado = (Vehiculo)dataGridVehiculo.SelectedItem;
                    controller.EliminarVehiculo(vehiculoSeleccionado.IdVehiculo);
                    CargarVehiculo();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID numérico válido para buscar.", "Error de Búsqueda");
                return;
            }

            List<Vehiculo> resultado = controller.BuscarVehiculo(id);
            dataGridVehiculo.ItemsSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún vehículo con ese ID.", "Búsqueda sin Resultado");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarVehiculo();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridVehiculo.SelectedItem != null && dataGridVehiculo.SelectedItem is Vehiculo vehiculo)
            {
                txtId.Text = vehiculo.IdVehiculo.ToString();
                txtPlaca.Text = vehiculo.Placa;
                txtMarca.Text = vehiculo.Marca;
                txtModelo.Text = vehiculo.Modelo;
                txtColor.Text = vehiculo.Color;
                txtAnio.Text = vehiculo.Año.ToString();
                txtIdDueño.Text = vehiculo.IdDueño.ToString();
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
