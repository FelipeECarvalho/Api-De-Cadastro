using CadastroApi.Data;
using CadastroApi.Extensions;
using CadastroApi.Models;
using CadastroApi.ViewModels;
using SecureIdentity.Password;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Controllers
{
    [ApiController]
    [Route("v1/accounts")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUserViewModel model, [FromServices] CadastroDataContext context) 
        {
            try 
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

                var user = new User
                {
                    Email = model.Email,
                    Cpf = model.Cpf,
                    Name = model.Name
                };

                var password = PasswordGenerator.Generate(25);
                user.PasswordHash = PasswordHasher.Hash(password);

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { user.Email, password }));

            }
            catch (DbUpdateException) 
            {
                return StatusCode(500, new ResultViewModel<User>("ExAcc50 - E-mail já existe"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<User>("ExAcc50 - Erro ao inserir usuário"));
            }
        }
    }
}
