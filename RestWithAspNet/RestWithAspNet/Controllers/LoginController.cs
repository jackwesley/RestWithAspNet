using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Business;
using RestWithAspNet.Model;
using Tapioca.HATEOAS;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }
               
        // POST api/values
        [AllowAnonymous]
        [HttpPost]  
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            return Ok(_loginBusiness.FindByLogin(user));

        }

    }
}