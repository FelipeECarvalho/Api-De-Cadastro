using Newtonsoft.Json;

namespace CadastroApi.ViewModels
{
    public class InputApiAddressViewModel
    {
        [JsonProperty("cep")]
        public string cep { get; set; }

        [JsonProperty("logradouro")]
        public string logradouro { get; set; }

        [JsonProperty("bairro")]
        public string bairro { get; set; }

        [JsonProperty("localidade")]
        public string localidade { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }
    }
}
