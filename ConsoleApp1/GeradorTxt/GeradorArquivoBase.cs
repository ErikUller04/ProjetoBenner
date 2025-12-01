using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using static Models.Entitys.Json.Data;

namespace GeradorTxt
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase
    {
        public virtual void Gerar(List<Empresa> empresas, string outputPath)
        {
            int empresaCount = 0, docCount = 0, itemCount = 0;

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
                    }
                }
            }
            sb.AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_00: " + empresaCount).AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_01: " + docCount).AppendLine()
            .Append("09|QUANTIDADE_LINHAS_TIPO_02: " + itemCount).AppendLine();

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        } 

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }
    }
}
