using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace FornecedoresApp
{
    public class ProdutoFornecedoresForm : Form
    {
        private TextBox txtProdutoId, txtFornecedorId, txtValorCompra;
        private Button btnSalvar, btnAtualizar, btnRemover;

        public ProdutoFornecedoresForm()
        {
            Text = "Gerenciar ProdutoFornecedores";

            txtProdutoId = new TextBox { Left = 10, Top = 10, Width = 200 };
            txtFornecedorId = new TextBox { Left = 10, Top = 40, Width = 200 };
            txtValorCompra = new TextBox { Left = 10, Top = 70, Width = 200 };

            btnSalvar = new Button { Text = "Salvar", Left = 10, Top = 100, Width = 200 };
            btnAtualizar = new Button { Text = "Atualizar", Left = 10, Top = 130, Width = 200 };
            btnRemover = new Button { Text = "Remover", Left = 10, Top = 160, Width = 200 };

            btnSalvar.Click += BtnSalvar_Click;
            btnAtualizar.Click += BtnAtualizar_Click;
            btnRemover.Click += BtnRemover_Click;

            Controls.Add(new Label { Text = "Produto ID", Left = 220, Top = 10 });
            Controls.Add(new Label { Text = "Fornecedor ID", Left = 220, Top = 40 });
            Controls.Add(new Label { Text = "Valor de Compra", Left = 220, Top = 70 });

            Controls.Add(txtProdutoId);
            Controls.Add(txtFornecedorId);
            Controls.Add(txtValorCompra);
            Controls.Add(btnSalvar);
            Controls.Add(btnAtualizar);
            Controls.Add(btnRemover);
        }

        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            var produtoFornecedor = new
            {
                produtoId = int.Parse(txtProdutoId.Text),
                fornecedorId = int.Parse(txtFornecedorId.Text),
                valorCompra = decimal.Parse(txtValorCompra.Text)
            };

            var json = JsonSerializer.Serialize(produtoFornecedor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:7208/api/ProdutoFornecedores", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("ProdutoFornecedor salvo com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao salvar ProdutoFornecedor.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro ao conectar com a API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}");
            }
        }

        private async void BtnAtualizar_Click(object sender, EventArgs e)
        {
            var produtoFornecedor = new
            {
                produtoId = int.Parse(txtProdutoId.Text),
                fornecedorId = int.Parse(txtFornecedorId.Text),
                valorCompra = decimal.Parse(txtValorCompra.Text)
            };

            var json = JsonSerializer.Serialize(produtoFornecedor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsync($"https://localhost:7208/api/ProdutoFornecedores/{produtoFornecedor.produtoId}/{produtoFornecedor.fornecedorId}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("ProdutoFornecedor atualizado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar ProdutoFornecedor.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro ao conectar com a API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}");
            }
        }

        private async void BtnRemover_Click(object sender, EventArgs e)
        {
            var produtoId = txtProdutoId.Text;
            var fornecedorId = txtFornecedorId.Text;

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"https://localhost:7208/api/ProdutoFornecedores/{produtoId}/{fornecedorId}");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("ProdutoFornecedor removido com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover ProdutoFornecedor.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Erro ao conectar com a API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
