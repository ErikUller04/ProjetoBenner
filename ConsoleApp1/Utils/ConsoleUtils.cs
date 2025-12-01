using ConsoleApp1.Utils;
using Models.Entitys.Exceptions;
using Utils.Out;
using System;

namespace Utils.ConsoleUtils
{
    public class ConsoleUtils
    {

        #region private methods

        /// <summary>
        /// Inicializa o console interativo.
        /// </summary>
        private static void RunInteractiveConsole()
        {
            bool inLooping = true;

            while (inLooping)
            {
                Console.WriteLine("Opções disponíveis:");
                Console.WriteLine("1: Configurar diretório dos dados");
                Console.WriteLine("2: Configurar diretório de saída");
                Console.WriteLine("3: Gerar saída");
                Console.WriteLine("0: sair");

                Console.Write("Digite a opção desejada: ");
                string option = Console.ReadLine() ?? string.Empty;
                string path = string.Empty;

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.Write("Escreva o diretório onde será salvo os dados utilizados no programa:");
                            path = PathUtils.CheckPath(Console.ReadLine());

                            PathUtils.SaveDataPath(path);

                            break;

                        case "2":
                            Console.Write("Escreva o diretório onde será salvo a saída gerada no programa:");
                            path = PathUtils.CheckPath(Console.ReadLine());

                            PathUtils.SaveOutputPath(path);
                            break;

                        case "3":

                            Console.Write("Escolha a versão do laioute (Versões disponíveis: 1 e 2): ");
                            Int32.TryParse(Console.ReadLine(), out int version);
                            OutUtils.ProcessOut(version);             
                            break;

                        case "0":
                            inLooping = false;
                            break;

                        default:
                            throw new InvalidOptionException("Opção invalida.");

                    }
                }

                catch (InvalidPathException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOptionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Inicializa o programa.
        /// </summary>
        public static void Run()
        {
            try
            {
                PathUtils.InitializeConfigPath();

                RunInteractiveConsole();
            }
            catch (NewFileException ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }

        #endregion 

    }
}
