using Produtos.Business;
using Produtos.DataAccess;
using Produtos.Model;
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
        protected MainPage mainPage;
        ProdutoBL produtoBL_obj = new ProdutoBL();
        public VisualizaProdutosPage()
        {
            InitializeComponent();

            var conn = Conexao.Get();
            var lista = produtoBL_obj.List(conn);
            conn.Close();

            //ProdutoMD ultimoCadastrado = lista.LastOrDefault();
            //lblPrincipal.Text = $"{ultimoCadastrado.Descricao}: {ultimoCadastrado.Preco}";

            //lvProduto.ItemsSource = lista.Select(p => $"{p.Id}:{p.Descricao}:{p.Preco}:{p.Ativo}").ToList();
            //lvProduto.ItemTapped += ItemSelecionadoString;

            lvCustom.ItemsSource = lista;
            lvCustom.ItemTapped += ItemSelecionadoProdutoMD;
            //foreach (ProdutoMD md_obj in lista)
            //{
            //    lblPrincipal.Text = lblPrincipal.Text + $"{md_obj.Descricao}\t{md_obj.Preco}\n";
            //}
        }
        private void ItemSelecionadoProdutoMD(object sender, ItemTappedEventArgs args)
        {
            ProdutoMD produtoTapped_obj = (ProdutoMD)args.Item;
            App.Nav.PushAsync(new MainPage(produtoTapped_obj));
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

            App.Nav.PushAsync(new MainPage(md));
        }

        
        private void Button_Clicked(object sender, EventArgs e)
        {
            mainPage = new MainPage(new ProdutoMD());
            App.Nav.PushAsync(mainPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.mainPage != null && this.mainPage.mdSalvo != null)//FOI CADASTRADO COM SUCESSO
            {
                ObservableCollection<ProdutoMD> lista_Produto = (ObservableCollection<ProdutoMD>)lvCustom.ItemsSource;
                lista_Produto.Add(this.mainPage.mdSalvo);
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