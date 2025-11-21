using System.Text;
using System.Windows;
using UVV_fintech.Control;
using UVV_fintech.Model;

namespace UVV_fintech.View
{
    public partial class ClienteWindow : Window
    {
        private Conta? _contaLogada;
        private readonly ContaController _contaController = new ContaController();
        private readonly TransacaoController _transacaoController = new TransacaoController();

        public ClienteWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string numeroConta = TxtConta.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeroConta))
            {
                MessageBox.Show("Por favor, insira o número da conta.",
                                "Login",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            Conta? conta = _contaController.BuscarContaPeloNumero(numeroConta);

            if (conta == null)
            {
                MessageBox.Show("Conta não encontrada. Verifique o número e tente novamente.",
                                "Conta não encontrada",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            _contaLogada = conta;

            var tipoConta = conta.GetTipoConta();
            LblInfoCliente.Text = $"Bem-vindo, {conta.Cliente.Nome}! ({tipoConta})";

            LoginPanel.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private bool VerificarContaLogada()
        {
            if (_contaLogada == null)
            {
                MessageBox.Show("Nenhuma conta está logada. Faça o login novamente.",
                                "Sessão expirada",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                LoginPanel.Visibility = Visibility.Visible;
                MenuPanel.Visibility = Visibility.Collapsed;
                return false;
            }

            return true;
        }

        private void BtnDepositar_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContaLogada()) return;

            var telaTransacao = new TransacaoWindow(TransacaoWindow.TipoOperacao.Deposito, _contaLogada!);
            telaTransacao.Owner = this;
            telaTransacao.ShowDialog();
        }

        private void BtnSacar_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContaLogada()) return;

            var telaTransacao = new TransacaoWindow(TransacaoWindow.TipoOperacao.Saque, _contaLogada!);
            telaTransacao.Owner = this;
            telaTransacao.ShowDialog();
        }

        private void BtnTransferir_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContaLogada()) return;

            var telaTransacao = new TransacaoWindow(TransacaoWindow.TipoOperacao.Transferencia, _contaLogada!);
            telaTransacao.Owner = this;
            telaTransacao.ShowDialog();
        }

        private void BtnSaldo_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContaLogada()) return;

            var contaAtualizada = _contaController.BuscarContaPeloNumero(_contaLogada!.NumeroConta);

            if (contaAtualizada == null)
            {
                MessageBox.Show("Não foi possível carregar os dados da conta.",
                                "Erro",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            _contaLogada = contaAtualizada;

            MessageBox.Show($"Saldo atual: R$ {contaAtualizada.GetSaldo():N2}",
                            "Saldo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnExtrato_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContaLogada()) return;

            var numeroConta = _contaLogada!.NumeroConta;
            var transacoes = _transacaoController.ObterTransacoesContaControl(numeroConta);

            if (transacoes == null || !transacoes.Any())
            {
                MessageBox.Show("Nenhuma transação encontrada para esta conta.",
                                "Extrato",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Extrato da conta {numeroConta}");
            sb.AppendLine(new string('-', 40));

            foreach (var t in transacoes)
            {
                string tipo = t switch
                {
                    Depositar => "Depósito",
                    Sacar => "Saque",
                    Transferir => "Transferência",
                    _ => "Transação"
                };

                sb.AppendLine($"{t.DataHora:dd/MM/yyyy HH:mm}  -  {tipo}  -  R$ {t.Valor:N2}");
            }

            MessageBox.Show(sb.ToString(),
                            "Extrato",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
