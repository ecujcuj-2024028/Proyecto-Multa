using AppProyectoMulta.Controller;
using AppProyectoMulta.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AppProyectoMulta.View
{
    public partial class GestionAgente : Window
    {
        private AgenteController controller;

        public GestionAgente()
        {
            InitializeComponent();
            controller = new AgenteController();
            CargarAgentes();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuPrincipal = new MainWindow();
            menuPrincipal.Show();
            this.Close();
        }

        private void CargarAgentes()
        {
            dataGridAgentes.ItemsSource = controller.ObtenerAgentes();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = " (Automático) ";
            txtNombre.Text = "";
            txtRango.Text = "";
            txtSalario.Text = "";
            txtIdMunicipio.Text = "";
            dataGridAgentes.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtRango.Text) ||
                string.IsNullOrEmpty(txtSalario.Text) ||
                string.IsNullOrEmpty(txtIdMunicipio.Text))
            {
                MessageBox.Show("Ningún campo puede estar vacío.");
                return;
            }

            try
            {
                double salario;
                if (!double.TryParse(txtSalario.Text, out salario))
                {
                    MessageBox.Show("El salario debe ser un número válido.");
                    return;
                }

                int idMunicipio;
                if (!int.TryParse(txtIdMunicipio.Text, out idMunicipio))
                {
                    MessageBox.Show("El ID de municipio debe ser un número entero.");
                    return;
                }

                if (txtId.Text == " (Automático) ")
                {
                    controller.AgregarAgente(txtNombre.Text, txtRango.Text, salario, idMunicipio);
                    MessageBox.Show("Agente agregado con éxito.");
                }
                else
                {
                    int id = Convert.ToInt32(txtId.Text);
                    controller.ActualizarAgente(id, txtNombre.Text, txtRango.Text, salario, idMunicipio);
                    MessageBox.Show("Agente actualizado con éxito.");
                }

                CargarAgentes();
                BtnNuevo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAgentes.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un agente para eliminar.");
                return;
            }

            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este agente?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    Agente agenteSeleccionado = (Agente)dataGridAgentes.SelectedItem;
                    controller.EliminarAgente(agenteSeleccionado.IdAgente);

                    CargarAgentes();
                    BtnNuevo_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void DataGridAgentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridAgentes.SelectedItem != null && dataGridAgentes.SelectedItem is Agente agente)
            {
                txtId.Text = agente.IdAgente.ToString();
                txtNombre.Text = agente.NombreAgente;
                txtRango.Text = agente.Rango;
                txtSalario.Text = agente.Salario.ToString();
                txtIdMunicipio.Text = agente.IdMunicipio.ToString();
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text) || !int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("Ingrese un ID numérico válido para buscar.", "Error de Búsqueda");
                return;
            }

            List<Agente> resultado = controller.BuscarAgente(id);
            dataGridAgentes.ItemsSource = resultado;

            if (resultado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún agente con ese ID.", "Búsqueda sin resultado");
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            CargarAgentes();
        }

        private void txtBuscarId_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
