using Produtos.Business;
using Produtos.DataAccess;
using Produtos.Model;
using Produtos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Produtos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizaProdutosPage : ContentPage
    {
        protected CadastraProdutoPage CadastraProdutoPage;
        ProdutoBL produtoBL_obj = new ProdutoBL();
        public VisualizaProdutosPage()
        {
            InitializeComponent();

            var conn = Conexao.Get();
            var lista = produtoBL_obj.List(conn);
            conn.Close();

            lvCustom.ItemsSource = lista;
            lvCustom.ItemTapped += ItemSelecionadoProdutoMD;

        }
        private void ItemSelecionadoProdutoMD(object sender, ItemTappedEventArgs args)
        {
            ProdutoMD produtoTapped_obj = (ProdutoMD)args.Item;
            this.CadastraProdutoPage = new CadastraProdutoPage(produtoTapped_obj);
            App.Nav.PushAsync(CadastraProdutoPage);
        }
        private void ItemSelecionadoString(object sender, ItemTappedEventArgs e)
        {            
            //DisplayAlert("Selecionado:", e.Item.ToString(), "OK");
            var valores = e.Item.ToString().Split(':');
            var md = new ProdutoMD
            {
                Id = int.Parse(valores[0]),
                Descricao = valores[1],
                Preco = decimal.Parse(valores[2]),
                Ativo = bool.Parse(valores[3]),
            };

            App.Nav.PushAsync(new CadastraProdutoPage(md));
        }

        
        private void Button_Clicked(object sender, EventArgs e)
        {
            CadastraProdutoPage = new CadastraProdutoPage(new ProdutoMD());
            App.Nav.PushAsync(CadastraProdutoPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.CadastraProdutoPage != null &&
                this.CadastraProdutoPage.mdSalvo != null &&
                !this.CadastraProdutoPage.edicao)//FOI CADASTRADO COM SUCESSO
            {
                ObservableCollection<ProdutoMD> lista_Produto = (ObservableCollection<ProdutoMD>)lvCustom.ItemsSource;
                lista_Produto.Add(this.CadastraProdutoPage.mdSalvo);
            }
            else if (this.CadastraProdutoPage != null &&
                this.CadastraProdutoPage.mdSalvo != null &&
                this.CadastraProdutoPage.edicao)
            {
                ObservableCollection<ProdutoMD> lista_Produto = (ObservableCollection<ProdutoMD>)lvCustom.ItemsSource;
                ProdutoMD prodDaLista = lista_Produto.Where(p => p.Id == this.CadastraProdutoPage.mdSalvo.Id).FirstOrDefault();
                lista_Produto.Remove(prodDaLista);
                lista_Produto.Add(this.CadastraProdutoPage.mdSalvo);
            }
        }

        private void BtnAtivos_Clicked(object sender, EventArgs e)
        {
            var conn = Conexao.Get();
            lvCustom.ItemsSource = produtoBL_obj.List(conn);
            conn.Close();
        }

        private void BtnInativos_Clicked(object sender, EventArgs e)
        {
            var conn = Conexao.Get();
            lvCustom.ItemsSource = produtoBL_obj.List(conn, false);
            conn.Close();
        }

        private void SwtStatus_Toggled(object sender, ToggledEventArgs e)
        {
            var conn = Conexao.Get();
            lvCustom.ItemsSource = produtoBL_obj.List(conn, swtStatus.IsToggled);
            conn.Close();
        }
    }
}