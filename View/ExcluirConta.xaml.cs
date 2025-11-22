using System.Windows;
using UVV_fintech.Control;

namespace UVV_fintech.View
{
    public partial class ExcluirConta : Window
    {
        private readonly ContaController _contaController = new();

        public ExcluirConta()
        {
            InitializeComponent();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtContaExcluir.Text))
            {
                MessageBox.Show("Por favor, preencha o campo número conta antes de exluir a conta.",
                                "Campos obrigatórios",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var numeroConta = TxtContaExcluir.Text;

            if(_contaController.ExcluirContaControl(numeroConta))
            {
                MessageBox.Show("Conta exluida com sucesso...");
            }
            else
            {
                MessageBox.Show("Não foi possivel exluir a conta, verifique se o numero esta correto");
            }
        }
    }
}
