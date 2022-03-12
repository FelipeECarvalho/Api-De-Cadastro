using CadastroApi.ViewModels;
using Refit;

namespace CadastroApi.Interfaces
{
    public interface ICepApiInterface
    {
        [Get("/ws/{cep}/json/")]
        Task<InputApiAddressViewModel> GetAddressAsync(string cep);
    }
}
