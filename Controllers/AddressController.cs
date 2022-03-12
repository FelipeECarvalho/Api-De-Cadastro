using CadastroApi.Data;
using CadastroApi.Interfaces;
using CadastroApi.Models;
using CadastroApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Controllers
{
    [ApiController]
    [Route("v1/addresses")]
    public class AddressController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromServices] CadastroDataContext context) 
        {
            try 
            {
                var addresses = await context.Addresses.ToListAsync();

                return Ok(new ResultViewModel<List<Address>>(addresses));
            }
            catch (Exception) 
            {
                return StatusCode(500, new ResultViewModel<Address>("ExAcc500 - Erro ao acessar o servidor"));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] CadastroDataContext context)
        {
            try
            {
                var address = await context.Addresses.FindAsync(id);

                if (address is null)
                    return BadRequest(new ResultViewModel<Address>("ExAdd400 - Endereço não existe"));

                return Ok(new ResultViewModel<Address>(address));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Address>("ExAcc500 - Erro ao acessar o servidor"));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InputAddressViewModel model, [FromServices] CadastroDataContext context) 
        {
            try
            { 
                var cepClient = Refit.RestService.For<ICepApiInterface>(Configuration.BaseUrl);

                var addressModel = await cepClient.GetAddressAsync(model.ZipCode);

                var user = await context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

                if (user is null)
                    return BadRequest(new ResultViewModel<Address>("ExAdd400 - Erro ao inserir endereço, usuário inválido"));

                var address = new Address
                {
                    City = addressModel.localidade,
                    ZipCode = addressModel.cep,
                    Number = model.Number,
                    District = addressModel.bairro,
                    State = addressModel.uf,
                    Street = addressModel.logradouro,
                    User = user
                };

                await context.Addresses.AddAsync(address);
                await context.SaveChangesAsync();

                return Created($"v1/addresses/{address.Id}", new ResultViewModel<Address>(address));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Address>("ExAcc500 - Erro ao inserir endereço"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Address>("ExAcc500 - Erro ao acessar o servidor"));
            }
        }
    }
}
