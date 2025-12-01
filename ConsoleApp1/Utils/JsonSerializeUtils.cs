using ConsoleApp1.Utils;
using Models.Entitys.Exceptions;
using Models.Entitys.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Models.Entitys.Json.Data;

namespace Utils.Json
{
    public class JsonSerializeUtils
    {
        /// <summary>
        /// Obtem o arquivo dos dados e desserializa os dados contidos no json.
        /// </summary>
        /// <returns>Os dados desserializados na classe Data.</returns>
        /// <exception cref="InvalidPathException">Indica que não existe nenhum arquivo .json no diretório especificado.</exception>
        /// <exception cref="InvalidDataException">Indica que o arquivo esta mal formatado ou vazio.</exception>
        public static List<Empresa> DeserializeData(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);

            List<Empresa> data = JsonSerializer.Deserialize<List<Empresa>>(json) ?? throw new InvalidDataException("Parece que o arquivo json não esta formatado corretamente ou não contém dados.");

            data = Data.EnsureValuesCorrect(data);

            return data;
        }
    }
}
