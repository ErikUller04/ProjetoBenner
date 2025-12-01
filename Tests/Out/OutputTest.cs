using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Out;

namespace Tests.Out
{
    public class OutputTest
    {
        int version;

        [SetUp]
        public void Setup() 
        {
            PathUtils.InitializeConfigPath();
            version = 1;
        }

        /// <summary>
        /// Testa a rotina de gerar o relatório em txt na versão definida.
        /// </summary>
        [Test(Description = "Testa a rotina de gerar o relatório em txt na versão definida.")]
        public void GenerateOutputTest() 
        {
            OutUtils.ProcessOut(version);
        }
    }
}
