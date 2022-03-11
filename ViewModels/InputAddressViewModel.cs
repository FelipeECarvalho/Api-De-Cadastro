using CadastroApi.Models;
using Newtonsoft.Json;

namespace CadastroApi.ViewModels
{
    public class InputAddressViewModel
    {
        [JsonProperty("cep")]
        public string ZipCode { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        [JsonProperty("bairro")]
        public string District { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }
    }
}
