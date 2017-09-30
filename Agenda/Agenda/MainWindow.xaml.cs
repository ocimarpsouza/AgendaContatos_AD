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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agenda
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        String operacao = "";
        int tipoPesq;
        Contato c = new Contato();
        ContatoDao Dao = new ContatoDao();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Funcao(1);
        }

        private void btnIncluir_Click(object sender, RoutedEventArgs e)
        {
            Funcao(2);
            LimparCampos();
            operacao = "incluir";
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            MudatextBox();
            btnVoltar.Visibility = Visibility.Hidden;
            Funcao(2);
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Contato c = new Contato();
            c.Nome = txtNome.Text;
            c.Email = txtEmail.Text;
            c.FoneCel = txtCelular.Text;
            c.FoneRes = txtresidencial.Text;
            c.FoneCom = txtComercial.Text;
            c.Ender = txtEndereco.Text;
            c.Bairro = txtBairro.Text;
            c.Cidade = txtCidade.Text;
            c.Uf = txtUF.Text;
            c.Cep = txtCep.Text;

            if (operacao == "incluir")
            {
                Dao.Inserir(c);
            }
            else
            {
                c.Id = Convert.ToInt32(txtID.Text);
                Dao.Editar(c);
            }

            Funcao(1);

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Funcao(3);
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            tbControl.SelectedItem = grid;
            MudatextBox();
            Funcao(1);
            btnVoltar.Visibility = Visibility.Hidden;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Dao.excluir(Convert.ToInt32(txtID.Text));
            Funcao(1);
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            if (rbNome.IsChecked == true)
            {
                this.tipoPesq = 1;
            }
            else if (rbEmail.IsChecked == true)
            {
                this.tipoPesq = 2;
            }
            else
            {
                this.tipoPesq = 3;
            }

            dgContatos.ItemsSource = Dao.Pesquisar(tipoPesq, txtPesquisar.Text);
            lbQtd.Content = Dao.Pesquisar(tipoPesq, txtPesquisar.Text).Count + " Contato(s)";

        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dgContatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgContatos.SelectedIndex >= 0)
            {
                Contato c = (Contato)dgContatos.SelectedItem;
                txtID.Text = c.Id.ToString();
                txtNome.Text = c.Nome;
                txtEmail.Text = c.Email;
                txtCelular.Text = c.FoneCel;
                txtComercial.Text = c.FoneCom;
                txtresidencial.Text = c.FoneRes;
                txtEndereco.Text = c.Ender;
                txtBairro.Text = c.Bairro;
                txtCidade.Text = c.Cidade;
                txtUF.Text = c.Uf;
                txtCep.Text = c.Cep;
                tbControl.SelectedItem = dados;
                MudatextBox();
                btnVoltar.Visibility = Visibility.Visible;
                Funcao(4);
            }
        }


        private void Funcao(int oper)
        {
            btnAlterar.IsEnabled = false;
            btnIncluir.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnExcluir.IsEnabled = false;
            btnPesquisar.IsEnabled = false;
            btnSalvar.IsEnabled = false;
            btnSair.IsEnabled = false;
            rbEmail.IsEnabled = false;
            rbFone.IsEnabled = false;
            rbNome.IsEnabled = false;
            rbTodos.IsEnabled = false;

            // iniciando sistema
            if (oper == 1)
            {
                btnIncluir.IsEnabled = true;
                btnPesquisar.IsEnabled = true;
                btnSair.IsEnabled = true;
                tbPesq.IsEnabled = true;
                rbEmail.IsEnabled = true;
                rbFone.IsEnabled = true;
                rbNome.IsEnabled = true;
                rbTodos.IsEnabled = true;
                operacao = "";
                LimparCampos();
                grid.IsEnabled = true;
                dados.IsEnabled = false;
                tbControl.SelectedItem = grid;
                lbQtd.Content = "";
                if (Dao.listar().Count > 0)
                {
                    dgContatos.ItemsSource = Dao.listar();
                    lbQtd.Content = Dao.listar().Count + " Contato(s)";
                }
                else
                {
                    dgContatos.ItemsSource = "";
                }

                btnVoltar.Visibility = Visibility.Hidden;
            }
            // inserir ou alterar contato
            else if (oper == 2)
            {
                btnCancelar.IsEnabled = true;
                btnSalvar.IsEnabled = true;
                tbControl.SelectedItem = dados;
                grid.IsEnabled = false;
                tbPesq.IsEnabled = false;
                tbControl.SelectedItem = dados;
                if (!txtNome.IsEnabled)
                {
                    MudatextBox();
                }

            }
            // cancelar
            else if (oper == 3)
            {
                tbControl.SelectedItem = grid;
                btnIncluir.IsEnabled = true;
                btnPesquisar.IsEnabled = true;
                btnSair.IsEnabled = true;
                grid.IsEnabled = true;
                tbPesq.IsEnabled = true;
                LimparCampos();
            }
            // mostrar
            else if (oper == 4)
            {
                btnAlterar.IsEnabled = true;
                btnExcluir.IsEnabled = true;
                btnSair.IsEnabled = false;
            }

        }

        private void LimparCampos()
        {
            txtID.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtCelular.Clear();
            txtComercial.Clear();
            txtresidencial.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUF.Clear();
            txtCep.Clear();
        }

        private void MudatextBox()
        {
            txtNome.IsEnabled = !txtNome.IsEnabled;
            txtEmail.IsEnabled = !txtEmail.IsEnabled;
            txtCelular.IsEnabled = !txtCelular.IsEnabled;
            txtresidencial.IsEnabled = !txtresidencial.IsEnabled;
            txtComercial.IsEnabled = !txtComercial.IsEnabled;
            txtEndereco.IsEnabled = !txtEndereco.IsEnabled;
            txtBairro.IsEnabled = !txtBairro.IsEnabled;
            txtCidade.IsEnabled = !txtCidade.IsEnabled;
            txtUF.IsEnabled = !txtUF.IsEnabled;
            txtCep.IsEnabled = !txtCep.IsEnabled;
        }

        private void rbTodos_Checked(object sender, RoutedEventArgs e)
        {
            Funcao(1);
        }
    }
}
