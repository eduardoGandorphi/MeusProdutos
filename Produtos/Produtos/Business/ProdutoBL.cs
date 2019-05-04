using Produtos.DataAccess;
using Produtos.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Produtos.Business
{
    public class ProdutoBL
    {
        ProdutoDA da = new ProdutoDA();
        public ProdutoMD Create(SQLiteConnection conn, ProdutoMD md)
        {
            ValidarProdutoCriacao(md);
            return da.Create(conn,md);
        }

        private void ValidarProdutoCriacao(ProdutoMD md)
        {
            if (md == null)
                throw new Exception("O produto não foi informado.");

            if (md.Id != 0)
                throw new Exception("O id do produto não deve ser informado.");


            if (string.IsNullOrEmpty(md.Descricao))
                throw new Exception("A descrição do produto deve ser informada.");


            if (md.Preco <= 0)
                throw new Exception("O preço do produto não deve ser maior que 0.");            
        }

        public ObservableCollection<ProdutoMD> List(SQLiteConnection conn = null, bool ativo = true)
        {
            return da.List(conn, ativo);
        }

        public ProdutoMD Update(SQLiteConnection conn, ProdutoMD md_obj)
        {
            return da.Update(conn, md_obj);
        }

        public ProdutoMD Delete(SQLiteConnection conn, ProdutoMD md_obj)
        {
            return da.Delete(conn, md_obj);
        }
    }
}
