using System;
using System.IO;

namespace GeradorTxt
{
    /// <summary>
    /// Responsável por interagir com o usuário via console.
    /// </summary>
    public static class MainConsole
    {
        private static string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "base-dados.json");
        private static string _outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out");

        public static void Run()
        {
            Directory.CreateDirectory(_outputDir);
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine("1. Configurar arquivo .json (base de dados)");
                Console.WriteLine("2. Configurar diretório de output");
                Console.WriteLine("3. Gerar arquivo");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");

                var opt = Console.ReadLine();
                Console.WriteLine();

                switch (opt)
                {
                    case "1":
                        Console.Write("Informe o caminho completo do arquivo .json: ");
                        var jp = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(jp) && File.Exists(jp))
                        {
                            _jsonPath = jp;
                            Console.WriteLine("OK! JSON configurado: " + _jsonPath);
                        }
                        else
                        {
                            Console.WriteLine("Caminho inválido ou arquivo não encontrado.");
                        }
                        break;

                    case "2":
                        Console.Write("Informe o diretório de saída para o .txt: ");
                        var od = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(od))
                        {
                            _outputDir = od;
                            Directory.CreateDirectory(_outputDir);
                            Console.WriteLine("OK! Diretório de saída configurado: " + _outputDir);
                        }
                        else
                        {
                            Console.WriteLine("Diretório inválido.");
                        }
                        break;

                    case "3":
                        Console.Write("Gerar arquivo");
                        try
                        {
                            var gerador = new GeradorArquivoBase();

                            var dados = JsonRepository.LoadEmpresas(_jsonPath);

                            var fileName = $"saida_leiaute_versão 01_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                            var fullPath = Path.Combine(_outputDir, fileName);

                            gerador.Gerar(dados, fullPath);

                            Console.WriteLine("Arquivo gerado em: " + fullPath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao gerar arquivo: " + ex.Message);
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
