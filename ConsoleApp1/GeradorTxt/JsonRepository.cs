using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GeradorTxt
{
    /// <summary>
    /// Lê o arquivo JSON sem depender de pacotes externos (usa JavaScriptSerializer).
    /// </summary>
    public static class JsonRepository
    {
        public static List<Empresa> LoadEmpresas(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("Arquivo JSON não encontrado.", jsonPath);

            try
            {
                var json = File.ReadAllText(jsonPath);

                // Desserializa diretamente para a lista de empresas
                var empresas = JsonConvert.DeserializeObject<List<Empresa>>(json);

                if (empresas == null)
                    throw new Exception("O JSON não contém dados de empresas válidos.");

                return empresas;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao desserializar JSON. " +
                    "Garanta que o arquivo está no formato esperado. Detalhes: " + ex.Message, ex);
            }
        }
    }
}
