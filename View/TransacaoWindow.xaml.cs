using System.Windows;
using UVV_fintech.Control;
using UVV_fintech.Model;

namespace UVV_fintech.View
{
    public partial class TransacaoWindow : Window
    {
        public enum TipoOperacao
        {
            Deposito,
            Saque,
            Transferencia
        }

        private readonly ClienteController _clienteController = new ClienteController();
        private readonly TipoOperacao _operacao;
        private readonly Conta _contaLogada;

        public TransacaoWindow(TipoOperacao operacao, Conta contaLogada)
        {
            InitializeComponent();

            _operacao = operacao;
            _contaLogada = contaLogada ?? throw new ArgumentNullException(nameof(contaLogada));

            ConfigurarTela();
        }

        private void ConfigurarTela()
        {
            // Esconde por padrão, mostra só em transferência
            PanelContaDestino.Visibility = Visibility.Collapsed;

            switch (_operacao)
            {
                case TipoOperacao.Deposito:
                    TxtTitulo.Text = "Depósito";
                    btnConfirmar.Content = "Depositar";
                    break;

                case TipoOperacao.Saque:
                    TxtTitulo.Text = "Saque";
                    btnConfirmar.Content = "Sacar";
                    break;

                case TipoOperacao.Transferencia:
                    TxtTitulo.Text = "Transferência";
                    btnConfirmar.Content = "Transferir";
                    PanelContaDestino.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            string valorTexto = TxtValor.Text?.Trim() ?? string.Empty;
            string? contaDestino = TxtContaDestino.Text?.Trim();

            if (!decimal.TryParse(valorTexto, out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Digite um valor válido (maior que zero).",
                                "Valor inválido",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Se for transferência, precisa da conta destino
            if (_operacao == TipoOperacao.Transferencia && string.IsNullOrWhiteSpace(contaDestino))
            {
                MessageBox.Show("Digite o número da conta destino.",
                                "Conta destino",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            bool sucesso = _clienteController.SolicitarTransacao(
                (int)_operacao,
                _contaLogada,
                valor,
                contaDestino
            );

            if (!sucesso)
            {
                string msgErro = _operacao switch
                {
                    TipoOperacao.Saque =>
                        "Não foi possível realizar o saque. Verifique o saldo e tente novamente.",
                    TipoOperacao.Transferencia =>
                        "Não foi possível realizar a transferência. Verifique os dados e o saldo.",
                    _ =>
                        "Não foi possível processar a transação. Tente novamente."
                };

                MessageBox.Show(msgErro,
                                "Erro na transação",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Mensagem de sucesso
            string msgSucesso = _operacao switch
            {
                TipoOperacao.Deposito =>
                    $"Depósito de R$ {valor:N2} realizado com sucesso!",
                TipoOperacao.Saque =>
                    $"Saque de R$ {valor:N2} realizado com sucesso!",
                TipoOperacao.Transferencia =>
                    $"Transferência de R$ {valor:N2} para a conta {contaDestino} realizada com sucesso!",
                _ =>
                    "Transação realizada com sucesso!"
            };

            MessageBox.Show(msgSucesso,
                            "Sucesso",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            Close();
        }
    }
}
