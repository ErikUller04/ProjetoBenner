using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entitys.Json
{
    public class InitialFileConfig
    {
        [JsonPropertyName("data_path")]
        public string DataPath { get; set; } = string.Empty;

        [JsonPropertyName("output_path")]
        public string OutputPath { get; set; } = string.Empty;
    }
}
