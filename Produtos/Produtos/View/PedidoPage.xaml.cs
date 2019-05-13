using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Produtos.View
{
    public partial class PedidoPage : ContentPage
    {
        public PedidoPage()
        {
            InitializeComponent();
        }

        void tbiBuscaItem_Clicked(object sender, System.EventArgs e)
        {
            App.Nav.PushAsync(new EscolheProduto());
        }
    }
}
