using System.Collections.Generic;

namespace GeradorTxt
{
    public class Empresa
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public List<Documento> Documentos { get; set; }
    }

    public class Documento
    {
        public string Modelo { get; set; }
        public string Numero { get; set; }
        public decimal Valor { get; set; }
        public List<ItemDocumento> Itens { get; set; }
    }

    public class ItemDocumento
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
