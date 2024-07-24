using System;
using System.Windows.Forms;

namespace FornecedoresApp
{
    public class MainForm : Form
    {
        private Button btnFornecedores;
        private Button btnProdutos;
        private Button btnProdutoFornecedores;

        public MainForm()
        {
            btnFornecedores = new Button { Text = "Gerenciar Fornecedores", Left = 10, Width = 200, Top = 10 };
            btnProdutos = new Button { Text = "Gerenciar Produtos", Left = 10, Width = 200, Top = 40 };
            btnProdutoFornecedores = new Button { Text = "Gerenciar ProdutoFornecedores", Left = 10, Width = 200, Top = 70 };

            btnFornecedores.Click += BtnFornecedores_Click;
            btnProdutos.Click += BtnProdutos_Click;
            btnProdutoFornecedores.Click += BtnProdutoFornecedores_Click;

            Controls.Add(btnFornecedores);
            Controls.Add(btnProdutos);
            Controls.Add(btnProdutoFornecedores);
        }

        private void BtnFornecedores_Click(object sender, EventArgs e)
        {
            var form = new FornecedorForm();
            form.Show();
        }

        private void BtnProdutos_Click(object sender, EventArgs e)
        {
            var form = new ProdutoForm();
            form.Show();
        }

        private void BtnProdutoFornecedores_Click(object sender, EventArgs e)
        {
            var form = new ProdutoFornecedoresForm();
            form.Show();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
