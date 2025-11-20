using System.Windows;

namespace UVV_fintech.View
{
    /// <summary>
    /// Lógica interna para BancoWindow.xaml
    /// </summary>
    public partial class BancoWindow : Window
    {
        public BancoWindow()
        {
            InitializeComponent();
        }

        private void btnCriarConta_Click(object sender, RoutedEventArgs e)
        {
            var janelaCriarConta = new CriarConta
            {
                Owner = this
            };

            // Abre como modal: usuário cria a conta, fecha, e continua no BancoWindow
            janelaCriarConta.ShowDialog();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            // Fecha o painel do banco.
            // O MenuInicial volta a aparecer porque lá a gente registrou o evento Closed.
            this.Close();
        }

        private void btnGerenciarClientes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de gerenciamento de clientes ainda não implementada.",
                            "Em desenvolvimento",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void btnGerenciarTransacoes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de gerenciamento de transações ainda não implementada.",
                            "Em desenvolvimento",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void btnExcluirConta_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de exclusão de conta ainda não implementada.",
                            "Em desenvolvimento",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}
