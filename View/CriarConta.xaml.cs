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
        private readonly ContaControl _controller = new ContaControl();

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

            bool resultado = _controller.CriarConta(
                TxtNome.Text.Trim(),
                TxtCpf.Text.Trim(),
                TxtTelefone.Text.Trim(),
                TxtEndereco.Text.Trim(),
                tipo
            );

            if (resultado)
            {
                MessageBox.Show("Conta criada com sucesso!");
                Close();
            }
            else
            {
                MessageBox.Show("Já existe um cliente com esse CPF no sistema.");
            }
        }
    }
}
