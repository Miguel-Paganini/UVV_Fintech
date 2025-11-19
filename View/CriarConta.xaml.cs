using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UVV_fintech.Control;

namespace UVV_fintech.View
{
    /// <summary>
    /// Lógica interna para CiarConta.xaml
    /// </summary>
    public partial class CriarConta : Window
    {
        private readonly ContaController _controller = new ContaController();

        public CriarConta()
        {
            InitializeComponent();
        }
        private void CriarConta_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNome.Text) ||
                string.IsNullOrWhiteSpace(TxtCpf.Text) ||
                string.IsNullOrWhiteSpace(TxtTelefone.Text) ||
                string.IsNullOrWhiteSpace(TxtEndereco.Text) ||
                CbTipoConta.SelectedItem == null)
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de criar a conta.");
                return;
            }

            var tipo = (CbTipoConta.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "";

            int resultado = _controller.CriarConta(
                TxtNome.Text,
                TxtCpf.Text.Trim(),
                TxtTelefone.Text.Trim(),
                TxtEndereco.Text,
                tipo
            );

            switch (resultado)
            {
                case -1:
                    MessageBox.Show("Já existe um cliente com esse CPF no sistema.");
                    break;
                case 0:
                    MessageBox.Show("Erro ao criar a conta. Verifique os dados e tente novamente.");
                    break;
                case 1:
                    MessageBox.Show("Conta criada com sucesso!");
                    Close();
                    break;
            }
        }
    }
}
