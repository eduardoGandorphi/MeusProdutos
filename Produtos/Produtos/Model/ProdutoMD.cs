using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Produtos.Model
{
    public class ProdutoMD
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Descricao { get; set; }

        [NotNull]
        public decimal Preco { get; set; }

        [NotNull]
        public bool Ativo { get; set; }
    }
}
