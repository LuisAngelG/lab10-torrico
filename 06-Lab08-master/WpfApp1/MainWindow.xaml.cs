using Business;
using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BCustomer customerBusiness;

        public MainWindow()
        {
            InitializeComponent();
            customerBusiness = new BCustomer(); // Configura la cadena de conexión a la base de datos.
        }
        private void BuscarClientePorNombre_Click(object sender, RoutedEventArgs e)
        {
            string customerName = TextBoxNombreCliente.Text; // Obtener el nombre ingresado por el usuario

            // Llama a la función de la capa de negocio para obtener los productos por nombre
            List<Customer> customers = customerBusiness.GetCustomersByName(customerName);

            // Asigna los resultados al ListView
            ListViewCustomers.ItemsSource = customers;
        }

        private void InsertarClientePorNombre_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;

            Customer newCustomer = new Customer
            {
                Name = customerName,
                Address = address,
                Phone = phone,
            };

            customerBusiness.InsertCustomer(newCustomer);

            RefreshDataGrid();
        }

        private void ActualizarClientePorNombre_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewCustomers.SelectedItem is Customer CustomerUpdate)
            {
                int updatedId = Convert.ToInt32(txtId.Text);
                string updatedName = txtName.Text;
                string updatedAddress = txtAddress.Text;
                string updatedPhone = txtPhone.Text;
                bool updatedActive = chkActive.IsChecked ?? false;

                CustomerUpdate.Id = updatedId;
                CustomerUpdate.Name = updatedName;
                CustomerUpdate.Address = updatedAddress;
                CustomerUpdate.Phone = updatedPhone;
                CustomerUpdate.Active = updatedActive;

                customerBusiness.UpdateCustomer(CustomerUpdate);

                RefreshDataGrid();
            }
        }

        private void EliminarClientePorNombre_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewCustomers.SelectedItem is Customer selectedCustomer)
            {
                customerBusiness.DeleteCustomer(selectedCustomer.Id);

                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            string searchName = TextBoxNombreCliente.Text;
            List<Customer> customers = customerBusiness.GetCustomersByName(searchName);

            ListViewCustomers.ItemsSource = customers;
        }
    }
}