using GeradorTxt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.Entitys.Json.Data;

namespace ConsoleApp1.GeradorTxt
{
    public class GeradorArquivoNovo : GeradorArquivoBase
    {

        public override void Gerar(List<Empresa> empresas, string outputPath)
        {
            int empresaCount = 0, docCount = 0, itemCount = 0, categoriaCount = 0;

            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                EscreverTipo00(sb, emp);
                empresaCount++;

                foreach (var doc in emp.Documentos)
                {
                    EscreverTipo01(sb, doc);
                    docCount++;

                    foreach (var item in doc.Itens)
                    {
                        EscreverTipo02(sb, item);
                        itemCount++;

                        foreach (var categoria in item.Categorias)
                        {
                            EscreverTipo03(sb, categoria);
                            categoriaCount++;

                        }
                    }
                }
            }
            sb.AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_00: " + empresaCount).AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_01: " + docCount).AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_02: " + itemCount).AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_03: " + categoriaCount).AppendLine()
            .Append("99|QUANTIDADE_LINHAS_NO_ARQUIVO: " + (empresaCount + docCount + itemCount + categoriaCount));

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|NUMEROITEM|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Numero).Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }

        protected void EscreverTipo03(StringBuilder sb, CategoriaItem categoria)
        {
            // 03|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            sb.Append("03").Append("|")
              .Append(categoria.Numero).Append("|")
              .Append(categoria.Descricao).AppendLine();
        }
    }
}
