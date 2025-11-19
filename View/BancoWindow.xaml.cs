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
            var janelaCriarConta = new CriarConta();
            janelaCriarConta.Show();
            this.Close();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
