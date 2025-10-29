using System.Windows;
using System.Windows.Controls;
using AppProyectoMulta;
using AppProyectoMulta.Controllers;
using AppProyectoMulta.Models;
using System;

namespace AppProyectoMulta.View
{
    public partial class GestionMunicipios : Window
    {
        private MunicipioController controller;

        public GestionMunicipios()
        {
            InitializeComponent();

            controller = new MunicipioController();

            CargarMunicipios();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }

        private void CargarMunicipios()
        {
            dataGridMunicipios.ItemsSource = controller.ObtenerMunicipios();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = " (Automático) ";
            txtNombre.Text = "";
            dataGridMunicipios.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo 'Nombre' no puede estar vacío.");
                return;
            }

            try
            {
                if (txtId.Text == " (Automático) ")
                {
                    controller.AgregarMunicipio(txtNombre.Text);
                    MessageBox.Show("Municipio agregado con éxito.");
                }
                else
                {
                    int id = Convert.ToInt32(txtId.Text);
                    controller.ActualizarMunicipio(id, txtNombre.Text);
                    MessageBox.Show("Municipio actualizado con éxito.");
                }

                CargarMunicipios();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMunicipios.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un municipio para eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este municipio?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    Municipio municipioSeleccionado = (Municipio)dataGridMunicipios.SelectedItem;
                    controller.EliminarMunicipio(municipioSeleccionado.IdMunicipio);

                    CargarMunicipios();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void DataGridMunicipios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridMunicipios.SelectedItem != null && dataGridMunicipios.SelectedItem is Municipio municipio)
            {
                txtId.Text = municipio.IdMunicipio.ToString();
                txtNombre.Text = municipio.NombreMunicipio;
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Por favor, ingrese un ID numérico válido para buscar.", "Error de Búsqueda");
                return;
            }

            List<Municipio> resultado = controller.BuscarMunicipio(id);

            dataGridMunicipios.ItemsSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún municipio con ese ID.", "Búsqueda Sin Resultados");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarMunicipios();
        }
    }
}