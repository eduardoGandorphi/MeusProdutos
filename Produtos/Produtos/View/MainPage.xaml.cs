using Produtos.Business;
using Produtos.DataAccess;
using Produtos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Produtos
{
    public partial class MainPage : ContentPage
    {
        ProdutoBL produtoBL_obj = new ProdutoBL();
        ProdutoMD md_obj = new ProdutoMD();
        public ProdutoMD mdSalvo = null;

        bool edicao = false;
        public MainPage(ProdutoMD md_obj)
        {
            InitializeComponent();
            this.md_obj = md_obj;
            edicao = md_obj.Id > 0;

            if (edicao)
            {
                lblTitulo.Text = "Edição de produto";
                txtDescricao.Text = md_obj.Descricao;
                txtPreco.Text = md_obj.Preco.ToString();
                swtAtivo.IsToggled = md_obj.Ativo;
            }
            else
            {
                lblTitulo.Text = "Cadastro de produto";
                txtDescricao.Text = "";
                txtPreco.Text = "";
                swtAtivo.IsToggled = true; 
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            md_obj.Descricao = txtDescricao.Text;
            md_obj.Preco = decimal.Parse(txtPreco.Text);
            md_obj.Ativo = swtAtivo.IsToggled;      
    
            var conn = Conexao.Get();
            ProdutoMD cadastrado = null;

            if (edicao)
                cadastrado = produtoBL_obj.Update(conn, md_obj);
            else
                cadastrado = produtoBL_obj.Create(conn, md_obj);

            conn.Commit();
            conn.Close();
            if (cadastrado != null && cadastrado.Id > 0)
            {
                DisplayAlert("Alerta", "Produto Salvo.", "Ok");
                txtDescricao.Text = "";
                txtPreco.Text = "";
                swtAtivo.IsToggled = true;
                mdSalvo = cadastrado;
                App.Nav.PopAsync();
            }
            else
                DisplayAlert("Alerta", "Erro ao cadastrar produto.", "Ok");
        }

        private async void BtnApagar_Clicked(object sender, EventArgs e)
        {
            bool confirmou = await DisplayAlert("Alerta",
                "Deseja realmente apagar este item ?"
                , "Sim"
                , "Não");

            if (confirmou)
            {
                var conn = Conexao.Get();
                conn.BeginTransaction();
                produtoBL_obj.Delete(conn, this.md_obj);
                conn.Commit();
                conn.Close();
                App.Nav.PopAsync();
            }
        }
    }
}
