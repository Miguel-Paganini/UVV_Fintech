using System.Windows;
using System.Windows.Controls;
using UVV_fintech.Control;

namespace UVV_fintech.View
{
    /// <summary>
    /// Lógica interna para CriarConta.xaml
    /// </summary>
    public partial class CriarConta : Window
    {
        private readonly ContaController _contaController = new ContaController();

        public CriarConta()
        {
            InitializeComponent();
        }

        private void CriarConta_Click(object sender, RoutedEventArgs e)
        {
            // Validação básica de campos
            if (string.IsNullOrWhiteSpace(TxtNome.Text) ||
                string.IsNullOrWhiteSpace(TxtCpf.Text) ||
                string.IsNullOrWhiteSpace(TxtTelefone.Text) ||
                string.IsNullOrWhiteSpace(TxtEndereco.Text) ||
                CbTipoConta.SelectedItem == null)
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de criar a conta.",
                                "Campos obrigatórios",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Recupera o tipo da conta (CC / CP) a partir do Tag do ComboBoxItem
            var tipoTag = (CbTipoConta.SelectedItem as ComboBoxItem)?.Tag?.ToString();

            if (string.IsNullOrWhiteSpace(tipoTag))
            {
                MessageBox.Show("Selecione um tipo de conta válido.",
                                "Tipo de conta",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            int resultado = _contaController.CriarConta(
                TxtNome.Text.Trim(),
                TxtCpf.Text.Trim(),
                TxtTelefone.Text.Trim(),
                TxtEndereco.Text.Trim(),
                tipoTag
            );

            // Descrição amigável do tipo de conta
            string descricaoTipoConta = tipoTag == "CC" ? "Conta Corrente" : "Conta Poupança";

            switch (resultado)
            {
                case -1:
                    MessageBox.Show("Já existe um cliente com esse CPF no sistema.",
                                    "CPF duplicado",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    break;

                case 0:
                    MessageBox.Show("Erro ao criar a conta. Verifique os dados e tente novamente.",
                                    "Erro ao criar conta",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    break;

                case 1:
                    MessageBox.Show($"{descricaoTipoConta} criada com sucesso!",
                                    "Sucesso",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    Close();
                    break;
            }
        }
    }
}
