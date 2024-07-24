using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace FornecedoresApp
{
    public class FornecedorForm : Form
    {
        private TextBox txtId, txtNome, txtCnpj, txtEndereco, txtTelefone;
        private Button btnSalvar, btnAtualizar, btnRemover;

        public FornecedorForm()
        {
            Text = "Gerenciar Fornecedores";

            txtId = new TextBox { Left = 10, Top = 10, Width = 200 }; // Adicione este controle
            txtNome = new TextBox { Left = 10, Top = 40, Width = 200 };
            txtCnpj = new TextBox { Left = 10, Top = 70, Width = 200 };
            txtEndereco = new TextBox { Left = 10, Top = 100, Width = 200 };
            txtTelefone = new TextBox { Left = 10, Top = 130, Width = 200 };

            btnSalvar = new Button { Text = "Salvar", Left = 10, Top = 160, Width = 200 };
            btnAtualizar = new Button { Text = "Atualizar", Left = 10, Top = 190, Width = 200 };
            btnRemover = new Button { Text = "Remover", Left = 10, Top = 220, Width = 200 };

            btnSalvar.Click += BtnSalvar_Click;
            btnAtualizar.Click += BtnAtualizar_Click;
            btnRemover.Click += BtnRemover_Click;

            Controls.Add(new Label { Text = "Id", Left = 220, Top = 10 }); // Adicione este controle
            Controls.Add(new Label { Text = "Nome", Left = 220, Top = 40 });
            Controls.Add(new Label { Text = "CNPJ", Left = 220, Top = 70 });
            Controls.Add(new Label { Text = "Endereço", Left = 220, Top = 100 });
            Controls.Add(new Label { Text = "Telefone", Left = 220, Top = 130 });

            Controls.Add(txtId); // Adicione este controle
            Controls.Add(txtNome);
            Controls.Add(txtCnpj);
            Controls.Add(txtEndereco);
            Controls.Add(txtTelefone);
            Controls.Add(btnSalvar);
            Controls.Add(btnAtualizar);
            Controls.Add(btnRemover);
        }

        private async void BtnSalvar_Click(object sender, EventArgs e)
        {
            var fornecedor = new
            {
                id = int.Parse(txtId.Text), // Adicione esta propriedade
                nome = txtNome.Text,
                cnpj = txtCnpj.Text,
                endereco = txtEndereco.Text,
                telefone = txtTelefone.Text,
                produtos = new object[] { }
            };

            var json = JsonSerializer.Serialize(fornecedor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:7208/api/Fornecedores", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Fornecedor salvo com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao salvar fornecedor.");
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
            var fornecedor = new
            {
                id = int.Parse(txtId.Text), // Adicione esta propriedade
                nome = txtNome.Text,
                cnpj = txtCnpj.Text,
                endereco = txtEndereco.Text,
                telefone = txtTelefone.Text,
                produtos = new object[] { }
            };

            var json = JsonSerializer.Serialize(fornecedor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsync("https://localhost:7208/api/Fornecedores/" + fornecedor.id, content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Fornecedor atualizado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar fornecedor.");
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
                    var response = await client.DeleteAsync("https://localhost:7208/api/Fornecedores/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Fornecedor removido com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao remover fornecedor.");
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
