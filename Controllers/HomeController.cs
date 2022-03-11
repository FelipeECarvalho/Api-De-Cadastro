using Microsoft.AspNetCore.Mvc;

namespace CadastroApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
            => Ok();
    }
}
