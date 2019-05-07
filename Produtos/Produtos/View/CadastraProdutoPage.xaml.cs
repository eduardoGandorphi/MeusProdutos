using Produtos.Business;
using Produtos.DataAccess;
using Produtos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Produtos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastraProdutoPage : ContentPage
    {
        ProdutoBL produtoBL_obj = new ProdutoBL();
        ProdutoMD md_Obj = new ProdutoMD();
        public ProdutoMD mdSalvo;
        public bool edicao;

        public CadastraProdutoPage(ProdutoMD md_Obj)
        {
            InitializeComponent();
            this.md_Obj = md_Obj;
            edicao = (md_Obj.Id != 0);

            if (edicao)
            {
                tcTexto.Text = "Edição";
                ecDescricao.Text = md_Obj.Descricao;
                ecPreco.Text = md_Obj.Preco.ToString();
                scAtivo.On = md_Obj.Ativo;
                this.AdicionarBotaoDeExclusaoDeProduto();
            }
            else
            {
                tcTexto.Text = "Cadastro";
                ecDescricao.Text = "";
                ecPreco.Text = "";
                scAtivo.On = true;
            }
            ecPreco.Keyboard = Keyboard.Numeric;

        }

        private void AdicionarBotaoDeExclusaoDeProduto()
        {
            var tbiDelete = new ToolbarItem()
            {
                Text = "Apagar".ToString(),
                Command = new Command(DeleteProduto),
            };

            this.ToolbarItems.Add(tbiDelete);
        }
        private async void DeleteProduto()
        {
            bool confirmou = await DisplayAlert("Alerta",
               "Deseja realmente apagar este item ?"
               , "Sim"
               , "Não");

            if (confirmou)
            {
                var conn = Conexao.Get();
                conn.BeginTransaction();
                produtoBL_obj.Delete(conn, this.md_Obj);
                conn.Commit();
                conn.Close();
                App.Nav.PopAsync();
            }
        }

        private void TbiSalvar_Clicked(object sender, EventArgs e)
        {
            md_Obj.Descricao = ecDescricao.Text;
            md_Obj.Preco = decimal.Parse(ecPreco.Text);
            md_Obj.Ativo = scAtivo.On;

            var conn = Conexao.Get();
            ProdutoMD cadastrado = null;

            if (edicao)
                cadastrado = produtoBL_obj.Update(conn, md_Obj);
            else
                cadastrado = produtoBL_obj.Create(conn, md_Obj);

            conn.Commit();
            conn.Close();
            if (cadastrado != null && cadastrado.Id > 0)
            {
                DisplayAlert("Alerta", "Produto Salvo.", "Ok");
                ecDescricao.Text = "";
                ecPreco.Text = "";
                scAtivo.On = true;
                mdSalvo = cadastrado;
                App.Nav.PopAsync();
            }
            else
                DisplayAlert("Alerta", "Erro ao cadastrar produto.", "Ok");
        }
    }
}