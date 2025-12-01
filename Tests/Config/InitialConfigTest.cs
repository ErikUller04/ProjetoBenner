using ConsoleApp1.Utils;
using Models.Entitys.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Config
{
    public class InitialConfigTest
    {
        string dataPath, outputPath;

        [SetUp]
        public void Setup()
        {
            PathUtils.InitializeConfigPath();
            dataPath = "D:\\Projeto_ToDo\\AvaliacaoDotNet\\ConsoleApp1\\Data";
            outputPath = "D:\\Projeto_ToDo\\AvaliacaoDotNet\\ConsoleApp1\\Out";
        }

        /// <summary>
        /// Testa a rotina de salvar o caminho de entrada de dados do programa.
        /// </summary>
        [Test(Description = "Testa a rotina de salvar o caminho de entrada de dados do programa.")]
        public void SaveDataPathTest()
        {
            PathUtils.SaveDataPath(dataPath);
        }

        /// <summary>
        /// Testa a rotina de salvar o caminho de saída do programa.
        /// </summary>
        [Test(Description = "Testa a rotina de salvar o caminho de saída do programa.")]
        public void SaveOutPathTest()
        {
            PathUtils.SaveOutputPath(outputPath);
        }


        /// <summary>
        /// Testa se os diretórios de entrada e saída são os padrões.
        /// </summary>
        [Test(Description = "Testa se os diretórios de entrada e saída são os padrões.")]
        public void CheckDefaultConfigTest()
        {
           InitialFileConfig config = PathUtils.GetFileConfig();
            
            // caminho padrão
            Assert.That(dataPath, Is.EqualTo(config.DataPath));
            Assert.That(outputPath, Is.EqualTo(config.OutputPath));

            Console.WriteLine("Diretório de entrada e saída estão configurados no padrão.");
        }

    }
}
