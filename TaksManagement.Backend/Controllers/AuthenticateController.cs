using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities;
using TaksManagement.Backend.Models;
using TaksManagement.Backend.Services;

namespace TaksManagement.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("authenticate")]
    public class AuthenticateController : Controller
    {
        private readonly AuthenticateAppService service;

        public AuthenticateController(AuthenticateAppService _service)
        {
            service = _service;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel login)
        {
            var token = service.Authenticate(login);

            if(token is null)
            {
                return Unauthorized();
            }

            return Ok(new {token});
        }
    }
}
