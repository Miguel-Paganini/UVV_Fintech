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
        }
    }
}
