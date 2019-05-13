using Produtos.DataAccess;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Produtos
{
    public partial class App : Application
    {
        public static NavigationPage Nav;
        public App()
        {
            InitializeComponent();
            var MinhaClasseConexao = new Conexao();
            MinhaClasseConexao.CriaEstruturaBanco();


            Nav = new NavigationPage(new VisualizaProdutosPage());
            MainPage = Nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
