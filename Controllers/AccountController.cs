using CadastroApi.Data;
using CadastroApi.Extensions;
using CadastroApi.Models;
using CadastroApi.ViewModels;
using SecureIdentity.Password;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CadastroApi.Services;

namespace CadastroApi.Controllers
{
    [ApiController]
    [Route("v1/accounts")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody]CreateUserViewModel model, [FromServices] CadastroDataContext context) 
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
        
        
        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model, [FromServices] CadastroDataContext context) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

                var user = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user is null)
                    return BadRequest(new ResultViewModel<User>("ExAcc40 - Usuário ou senha inválidos"));

                if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                    return BadRequest(new ResultViewModel<User>("ExAcc40 - Usuário ou senha inválidos"));

                var token = new TokenService().GetToken(user);

                return Ok(new ResultViewModel<string>(token, new()));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("ExAcc50 - Erro ao acessar usuário"));
            }
        }
    }
}
