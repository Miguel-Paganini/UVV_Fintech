using System.Linq;
using System.Windows;
using UVV_fintech.Control;

namespace UVV_fintech.View
{
    /// <summary>
    /// Lógica interna para ManageClienteWindow.xaml
    /// </summary>
    public partial class ManageClienteWindow : Window
    {
        private readonly ClienteController _clienteController = new ClienteController();

        public ManageClienteWindow()
        {
            InitializeComponent();
        }

        private void btnListClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var clientes = _clienteController.ListarClientes();

                if (clientes == null || !clientes.Any())
                {
                    MessageBox.Show("Nenhum cliente encontrado no sistema.",
                                    "Lista de clientes",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    lstBoxClientes.ItemsSource = null;
                    return;
                }

                lstBoxClientes.ItemsSource = clientes;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao listar clientes: {ex.Message}",
                                "Erro",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
