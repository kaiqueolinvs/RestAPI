using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPIPessoa.Application.Autenticacao;

namespace WebAPIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost]
        public IActionResult LOGIN([FromBody] autenticacaorequest request )
        {
            var autenticacaoService = new AutenticacaoSevice();
            var tokenString = autenticacaoService.Autenticar(request);

              if(string.IsNullOrWhiteSpace( tokenString ))
            {
                return Unauthorized();
            }
            else
            {
                 return  Ok (new { token = tokenString });
            }
             
        }
       
    }
}
