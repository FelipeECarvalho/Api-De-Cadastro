using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace CadastroApi.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary value) 
        {
            var result = new List<string>();
            foreach (var item in value.Values)
                result.AddRange(item.Errors.Select(x => x.ErrorMessage));

            return result;
        }
    }
}
