using Produtos.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Produtos.DataAccess
{
    public class ProdutoDA
    {
        public ProdutoMD Create(SQLiteConnection conn, ProdutoMD md)
        {
            conn.Insert(md);
            return conn.Table<ProdutoMD>().LastOrDefault();
        }

        public ObservableCollection<ProdutoMD> List(SQLiteConnection conn, bool ativo)
        {

            if (conn == null)
                conn = Conexao.Get();

            List<ProdutoMD> listaDeProduto = conn
                .Table<ProdutoMD>()
                .Where(p => p.Ativo == ativo)
                .ToList();

            return new ObservableCollection<ProdutoMD>(listaDeProduto);
        }

        public ProdutoMD Update(SQLiteConnection conn, ProdutoMD md_obj)
        {
            conn.Update(md_obj);
            return conn.Table<ProdutoMD>().Where(p=>p.Id == md_obj.Id).FirstOrDefault();
        }

        public ProdutoMD Delete(SQLiteConnection conn, ProdutoMD md_obj)
        {
            conn.Delete(md_obj);
            return md_obj;
        }
    }
}
