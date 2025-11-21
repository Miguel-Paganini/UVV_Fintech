using System.Collections.ObjectModel;
using System.Windows;
using UVV_fintech.Control;
using UVV_fintech.Model;

namespace UVV_fintech.View
{
    public partial class GerenciarTransacoes : Window
    {
        public ObservableCollection<Transacao> ListaTransacoes { get; set; }
            = new ObservableCollection<Transacao>();

        private readonly TransacaoController _transacaoController = new();

        public GerenciarTransacoes()
        {
            InitializeComponent();

            dgTransacoes.DataContext = this;

            CarregarTransacoes();
        }

        private void CarregarTransacoes()
        {
            ListaTransacoes.Clear();

            foreach (var transacao in _transacaoController.ObterListaTransacoes())
            {
                ListaTransacoes.Add(transacao);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarTransacoes();
        }
    }
}
