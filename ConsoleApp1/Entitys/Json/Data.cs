using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entitys.Json
{
    public class Data
    {
        public class Empresa 
        {
            [JsonPropertyName("cnpj")]
            public string CNPJ { get; set; } = string.Empty;

            [JsonPropertyName("nome")]
            public string Nome { get; set; } = string.Empty;

            [JsonPropertyName("telefone")]
            public string Telefone { get; set; } = string.Empty;

            [JsonPropertyName("documentos")]
            public List<Documento> Documentos { get; set; }
            
        }
        
        public class Documento 
        {
            [JsonPropertyName("modelo")]
            public string Modelo { get; set; } = string.Empty;

            [JsonPropertyName("numero")]
            public string Numero { get; set; } = string.Empty;

            [JsonPropertyName("valor")]
            public decimal Valor {  get; set; } = 0m;

            [JsonPropertyName("itens")]
            public List<ItemDocumento> Itens { get; set; }
        }

        public class ItemDocumento
        {
            [JsonPropertyName("numeroItem")]
            public int Numero { get; set; }

            [JsonPropertyName("descricao")]
            public string Descricao { get; set; } = string.Empty;

            [JsonPropertyName("valor")]
            public decimal Valor { get; set; } = 0m;

            [JsonPropertyName("categorias")]
            public List<CategoriaItem> Categorias { get; set; }
        }

        public class CategoriaItem
        {
            [JsonPropertyName("numeroCategoria")]
            public int? Numero { get; set; }

            [JsonPropertyName("descricaoCategoria")]
            public string Descricao { get; set; }
        }


        /// <summary>
        /// Garante que o valor de documento corresponda a soma de todos os valores de seus itens documentos.
        /// </summary>
        /// <param name="empresas">Os dados que serão utilizados para gerar o resultado.</param>
        /// <returns>Os dados com os valores dos documentos corretos.</returns>
        public static List<Empresa> EnsureValuesCorrect(List<Empresa> empresas)
        {
            foreach (var empresa in empresas)
            {
                foreach (var doc in empresa.Documentos)
                {
                    doc.Valor = doc.Itens.Select(x => x.Valor).Sum();
                }
            }

            return empresas;
        }
    }
}
