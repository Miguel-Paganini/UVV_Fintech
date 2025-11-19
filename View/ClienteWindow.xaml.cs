using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UVV_fintech.Control;
using UVV_fintech.Model;

namespace UVV_fintech.View
{
    /// <summary>
    /// Lógica interna para ClienteWindow.xaml
    /// </summary>
    public partial class ClienteWindow : Window
    {
        private Conta? _contaLogada;
        private readonly ContaController _contaController = new ContaController();
        public ClienteWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string numeroConta = TxtConta.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeroConta))
            {
                MessageBox.Show("Por favor, insira o número da conta.");
                return;
            }

            Conta? conta = _contaController.BuscarContaPeloNumero(numeroConta);

            if (conta == null)
            {
                MessageBox.Show("Conta não encontrada. Verifique o número e tente novamente.");
                return;
            }

            _contaLogada = conta;

            LblInfoCliente.Text = $"Bem-vindo, {_contaLogada?.Cliente.Nome}!";

            LoginPanel.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void BtnDepositar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Função Depositar (implementar)");
        }

        private void BtnSacar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Função Sacar (implementar)");
        }

        private void BtnTransferir_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Função Transferir (implementar)");
        }

        private void BtnSaldo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Saldo atual: R$ {_contaLogada?.Saldo}");
        }

        private void BtnExtrato_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exibir extrato (implementar)");
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;

            TxtConta.Text = "";
            _contaLogada = null;
        }
    }
}
