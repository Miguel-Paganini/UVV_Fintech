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
            janelaCriarConta.ShowDialog();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGerenciarClientes_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnGerenciarTransacoes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExcluirConta_Click(object sender, RoutedEventArgs e)
        {
            var janelaExluirConta = new ExcluirConta
            {
                Owner = this
            };
            janelaExluirConta.ShowDialog();
        }
    }
}
