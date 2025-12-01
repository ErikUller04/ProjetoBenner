using Models.Entitys.Exceptions;
using Models.Entitys.Json;
using System;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1.Utils
{
    public class PathUtils
    {
        #region propertys

        /// <summary>
        /// Caminho do arquivo de configuração.
        /// </summary>
        private static string ConfigFilePath = string.Empty;

        #endregion

        #region private methods

        /// <summary>
        /// Obtem o caminho do arquivo de configuração.
        /// </summary>
        /// <returns>O caminho do arquivo de configuração</returns>
        /// <exception cref="NewFileException">Indica que o arquivo não existia e foi criado um novo.</exception>
        private static string GetFilePath()
        {
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.

            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()) // volta da pasta netX.x -> Debug
                            .Parent // volta da pasta Debug -> bin
                            .Parent // volta da pasta bin -> raiz projeto 
                            .FullName;

            string filePath = basePath + "\\config.json";
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            if (!File.Exists(filePath))
            {
                SetfileConfig(new InitialFileConfig(), filePath);

                throw new NewFileException($"Arquivo de configuração inicial criado em: {filePath}, por favor crie e configure os arquivos necessários.");
            }

            return filePath;
        }

        /// <summary>
        /// Salva as configurações no arquivo de configuração do programa.
        /// </summary>
        /// <param name="fileConfig">A configuração.</param>
        /// <param name="initialFile">O arquivo de configuração quando for o inicial criado zerado.</param>
        private static void SetfileConfig(InitialFileConfig fileConfig, string initialFile = "")
        {
            string jsonContent = JsonSerializer.Serialize(fileConfig, new JsonSerializerOptions { WriteIndented = true });

            if (!string.IsNullOrEmpty(initialFile))
            {
                File.WriteAllText(initialFile, jsonContent);
                return;
            }

            File.WriteAllText(ConfigFilePath, jsonContent);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Inicializa o diretório do arquivo de configuração.
        /// </summary>
        public static void InitializeConfigPath()
        {
            ConfigFilePath = GetFilePath();
        }

        /// <summary>
        /// Obtem o arquivo de configuração
        /// </summary>
        /// <returns>A configuração do programa</returns>
        public static InitialFileConfig GetFileConfig()
        {
            string json = File.ReadAllText(ConfigFilePath);

            InitialFileConfig initialFileConfig = JsonSerializer.Deserialize<InitialFileConfig>(json) ?? new InitialFileConfig();

            return initialFileConfig;
        }

        /// <summary>
        /// Salva o diretório de entrada utilizado no programa no arquivo de configuração.
        /// </summary>
        /// <param name="path">O diretório.</param>
        /// <exception cref="NewFileException">Indica que o diretório foi criado e pede para configurar o arquivo de dados.</exception>
        public static void SaveDataPath(string path)
        {

            InitialFileConfig initialFileConfig = GetFileConfig();
            initialFileConfig.DataPath = path;

            SetfileConfig(initialFileConfig);            

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                throw new NewFileException($"Diretório: {path} criado, por favor crie o arquivo com os dados para teste.");
            }

            Console.WriteLine($"Diretório de dados ({path}) salvo no arquivo de configuração.");

        }

        /// <summary>
        /// Salva o diretório de saída utilizado no programa no arquivo de configuração.
        /// </summary>
        /// <param name="path">O diretório.</param>
        public static void SaveOutputPath(string path)
        {
            InitialFileConfig initialFileConfig = GetFileConfig();
            initialFileConfig.OutputPath = path;

            SetfileConfig(initialFileConfig);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                Console.WriteLine($"Diretório de saída: {path} criado.");
            }

            Console.WriteLine($"Diretório de saída ({path}) salvo no arquivo de configuração.");
        }

        /// <summary>
        /// Verifica se o diretório do arquivo é válido.
        /// </summary>
        /// <param name="path">O diretório</param>
        /// <returns>O proprio diretório caso for válido</returns>
        /// <exception cref="InvalidPathException">Indica que o diretório não é valido.</exception>
        public static string CheckPath(string path)
        {
            if (!Path.IsPathRooted(path))
                throw new InvalidPathException($"O diretório passado ({path}) não é válido.");

            if (path.Contains("/"))
                path = path.Replace("/", "\\");

            if (path[2] != '\\')
                throw new InvalidPathException($"O diretório passado ({path}) não contém o caractere '\\'  após ':'");

            return path;
        } 

        #endregion

    }
}
