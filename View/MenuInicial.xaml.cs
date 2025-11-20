using System.Windows;

namespace UVV_fintech.View
{
    public partial class MenuInicial : Window
    {
        public MenuInicial()
        {
            InitializeComponent();
        }

        private void BtnMenuCliente_Click(object sender, RoutedEventArgs e)
        {
            var janelaCliente = new ClienteWindow
            {
                Owner = this
            };

            // Quando fechar a janela do cliente, volta a mostrar o menu inicial
            janelaCliente.Closed += (_, _) => this.Show();

            this.Hide();
            janelaCliente.Show();
        }

        private void BtnMenuBanco_Click(object sender, RoutedEventArgs e)
        {
            var janelaBanco = new BancoWindow
            {
                Owner = this
            };

            // Quando fechar a janela do banco, volta a mostrar o menu inicial
            janelaBanco.Closed += (_, _) => this.Show();

            this.Hide();
            janelaBanco.Show();
        }
    }
}
