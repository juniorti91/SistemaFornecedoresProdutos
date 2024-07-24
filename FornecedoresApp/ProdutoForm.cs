using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace FornecedoresApp
{
    public class ProdutoForm : Form
    {
        private TextBox txtId, txtDescricao, txtUnidadeDeMedida;
        private Button btnSalvar, btnAtualizar, btnRemover;

        public ProdutoForm()
        {
            Text = "Gerenciar Produtos";

            txtId = new TextBox { Left = 10, Top = 10, Width = 200 }; // Adicione este controle
            txtDescricao = new TextBox { Left = 10, Top = 40, Width = 200 };
            txtUnidadeDeMedida = new TextBox { Left = 10, Top = 70, Width = 200 };

            btnSalvar = new Button { Text = "Salvar", Left = 10, Top = 100, Width = 200 };
            btnAtualizar = new Button { Text = "Atualizar", Left = 10, Top = 130, Width = 200 };
            btnRemover = new Button { Text = "Remover", Left = 10, Top = 160, Width = 200 };

            btnSalvar.Click += BtnSalvar_Click;
            btnAtualizar.Click += BtnAtualizar_Click;
            btnRemover.Click += BtnRemover_Click;

            Controls.Add(new Label { Text = "Id", Left = 220, Top = 10 }); // Adicione este controle
            Controls.Add(new Label { Text = "Descrição", Left = 220, Top = 40 });
            Controls.Add(new Label { Text = "Unidade de Medida", Left = 220, Top = 70 });

            Controls.Add(txtId); // Adicione este controle
            Controls.Add(txtDescricao);
            Controls.Add(txtUnidadeDeMedida);
            Controls.Add(btnSalvar);
            Controls.Add(btnAtualizar);
            Controls.Add(btnRemover);
        }

        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            var produto = new
            {
                id = int.Parse(txtId.Text), // Adicione esta propriedade
                descricao = txtDescricao.Text,
                unidadeDeMedida = int.Parse(txtUnidadeDeMedida.Text)
            };

            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:7208/api/Produtos", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produto salvo com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao salvar produto.");
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
            var produto = new
            {
                id = int.Parse(txtId.Text), // Adicione esta propriedade
                descricao = txtDescricao.Text,
                unidadeDeMedida = int.Parse(txtUnidadeDeMedida.Text)
            };

            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsync("https://localhost:7208/api/Produtos/" + produto.id, content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produto atualizado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar produto.");
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
            var id = txtId.Text;

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync("https://localhost:7208/api/Produtos/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Produto removido com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover produto.");
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
