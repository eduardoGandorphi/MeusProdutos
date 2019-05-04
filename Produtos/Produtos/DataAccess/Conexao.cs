using PCLExt.FileStorage.Folders;
using Produtos.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Produtos.DataAccess
{
    public class Conexao{
        public static SQLiteConnection Get(){
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("meuBanco.db"
                , PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);
            return new SQLiteConnection(arquivo.Path);

            // return new SQLiteConnection(@"C:\temp\meuBanco.db");
        }
        public void CriaEstruturaBanco(){
            var conn = Get();
            conn.BeginTransaction();
            conn.CreateTable<ProdutoMD>();
            conn.Commit();
            conn.Close();
        }
    }
}
