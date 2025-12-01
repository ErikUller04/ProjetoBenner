using ConsoleApp1.GeradorTxt;
using ConsoleApp1.Utils;
using GeradorTxt;
using Models.Entitys.Exceptions;
using Models.Entitys.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Json;
using static Models.Entitys.Json.Data;

namespace Utils.Out
{
    public class OutUtils
    {


        /// <summary>
        /// Gera um arquivo de resultado novo de acordo com a versão.
        /// </summary>
        /// <param name="finalText">O texto a ser escrito no arquivo.</param>
        /// <param name="version">Versão escolhida para gerar o laioute.</param>
        private static string GenerateOut(InitialFileConfig config, int version)
        {

            int qtdResult = Directory.GetFiles(config.OutputPath).Where(x => x.Contains($"v{version}")).Count();

            string finalResultPath = $"{config.OutputPath}\\v{version}-result-{qtdResult + 1}.txt";

            return finalResultPath;
        }

        #region public methods

        /// <summary>
        /// Obtem o texto de acordo com a versão de laiout desejada e gera um arquivo no caminho de saída definido.
        /// </summary>
        /// <param name="version">versão que será gerado o laioute no arquivo de saída.</param>
        /// <exception cref="InvalidOptionException">Versão inválida.</exception>
        public static void ProcessOut(int version)
        {
            List<Empresa> data;
            string finalText = string.Empty, jsonPath = string.Empty, fileOutputPath = string.Empty;

            InitialFileConfig config = PathUtils.GetFileConfig();

            switch (version)
            {
                case 1:

                    jsonPath = Directory.GetFiles(config.DataPath).Where(x => x.Contains("base-dados.json")).FirstOrDefault() ?? throw new InvalidPathException("Por favor crie um arquivo json para ser a base dos dados");
                    data = JsonSerializeUtils.DeserializeData(jsonPath);

                    var gerador = new GeradorArquivoBase();

                    fileOutputPath = GenerateOut(config, version);

                    gerador.Gerar(data, fileOutputPath);

                    Console.WriteLine($"Arquivo criado em: {fileOutputPath}");
                    break;

                case 2:
                    jsonPath = Directory.GetFiles(config.DataPath).Where(x => x.Contains("base-dados-v2.json")).FirstOrDefault() ?? throw new InvalidPathException("Por favor crie um arquivo json para ser a base dos dados");
                    data = JsonSerializeUtils.DeserializeData(jsonPath);

                    var geradorNovo = new GeradorArquivoNovo();

                    fileOutputPath = GenerateOut(config, version);

                    geradorNovo.Gerar(data, fileOutputPath);

                    Console.WriteLine($"Arquivo criado em: {fileOutputPath}");
                    break;

                default:
                    throw new InvalidOptionException("Versão inválida.");
            }

        }

        #endregion
    }
}
