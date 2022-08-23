using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaksManagement.Backend.Models;
using TaksManagement.Backend.Services;

namespace TaksManagement.Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAppService service;

        public UserController(UserAppService _service)
        {
            service = _service;
        }

        [HttpGet("list")]
        public ActionResult<List<UserModel>> GetUsers()
        {
            return service.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUser(string id)
        {
            return service.GetUser(id);
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel user)
        {
            try
            {
                service.Create(user);
                return Created(nameof(user), user);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody] UserModel userUpdate)
        {
            service.Update(userUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            service.Delete(id);
            return NoContent();
        }

    }
}
